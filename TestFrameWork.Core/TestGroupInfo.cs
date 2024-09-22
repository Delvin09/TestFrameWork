using System.Collections.Immutable;
using System.Reflection;
using TestFrameWork.Abstractions;
using TestFrameWork.Abstractions.EventArgs;
using TestFrameWork.Abstractions.Results;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        private static readonly TestState[] _beforeTestStatus = [TestState.None, TestState.Pending, TestState.Running];

        private readonly ILogger _logger;

        public TestGroupInfo(ILogger logger)
        {
            _logger = logger;
        }

        public string Name { get; init; } = string.Empty;

        public required Type Type { get; internal init; }

        public ImmutableArray<TestInfo> Tests { get; init; }

        public MethodInfo? Initializer { get; internal init; }

        public TestGroupResult Run()
        {
            var result = new TestGroupResult(Name);
            try
            {
                _logger.LogInfo($"Start test group `{Name}`. Init test class object.");

                if (Type == null) throw new InvalidOperationException($"Test Class [{Name}] can't be found!");

                var instance = Activator.CreateInstance(Type);
                if (instance == null) throw new InvalidOperationException("Test Class isn't created.");

                Initializer?.Invoke(instance, null);

                foreach (var test in Tests)
                {
                    test.TestStateChanged += Test_TestStateChanged;

                    try
                    {
                        result.AddTestResult(test.Run(instance));
                    }
                    finally
                    {
                        test.TestStateChanged -= Test_TestStateChanged;
                    }
                }

                _logger.LogInfo($"End test group `{Name}`.");
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                _logger.LogError(ex);
            }

            return result;
        }

        private void Test_TestStateChanged(object? sender, TestEventArgs e)
        {
            _logger.LogInfo($"Test status of `{e.TestName}` was changed from `{e.OldState}` to `{e.NewState}`");

            if (_beforeTestStatus.Contains(e.NewState))
            {
                BeforeTestRun?.Invoke(this, e);
            }
            else
            {
                AfterTestRun?.Invoke(this, e);
            }
        }

        public event EventHandler<TestEventArgs>? BeforeTestRun;
        public event EventHandler<TestEventArgs>? AfterTestRun;
    }
}
