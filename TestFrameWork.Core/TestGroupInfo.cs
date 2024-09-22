using System.Collections.Immutable;
using System.Reflection;
using TestFrameWork.Abstractions;
using TestFrameWork.Abstractions.EventArgs;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; init; } = string.Empty;

        public required Type Type { get; internal init; }

        public ImmutableArray<TestInfo> Tests { get; init; }

        public MethodInfo? Initializer { get; internal init; }

        public TestGroupResult Run()
        {
            var result = new TestGroupResult(Name);
            try
            {
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
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        private void Test_TestStateChanged(object? sender, TestEventArgs e)
        {
            var beforeTestStatus = new[] { TestState.None, TestState.Pending, TestState.Running };
            if (beforeTestStatus.Contains(TestState.Pending))
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
