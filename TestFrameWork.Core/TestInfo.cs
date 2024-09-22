using System.Diagnostics;
using System.Reflection;
using TestFrameWork.Abstractions;
using TestFrameWork.Abstractions.EventArgs;
using TestFrameWork.Abstractions.Results;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestInfo
    {
        private readonly string _assemblyName = string.Empty;
        private readonly string _groupName = string.Empty;
        private readonly ILogger _logger;

        private TestState _testState = TestState.Pending;
        private string? _message;

        public string Name { get; init; } = string.Empty;

        public MethodInfo? Method { get; init; }

        public TestState State
        {
            get => _testState;
            set
            {
                if (_testState != value && value != TestState.None)
                {
                    var oldState = _testState;
                    _testState = value;

                    OnTestStateChanged(oldState);
                }
            }
        }

        public TestInfo(ILogger logger)
        {
            _logger = logger;
        }

        private void OnTestStateChanged(TestState oldState)
        {
            try
            {
                TestStateChanged?.Invoke(
                    this,
                    new TestEventArgs
                    {
                        AssemblyName = _assemblyName,
                        GroupName = _groupName,
                        TestName = Name,
                        NewState = _testState,
                        OldState = oldState,
                        Message = _message
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public TestResult Run(object subject)
        {
            _logger.LogInfo($"Run test `{Name}`");

            Exception? exception = null;
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                State = TestState.Running;

                Method?.Invoke(subject, []);

                State = TestState.Success;
            }
            catch (TargetInvocationException ex) when (ex.InnerException is AssertionFailException)
            {
                _message = ex.InnerException.Message;
                State = TestState.Failed;

                _logger.LogWarning($"Test `{Name}` assertion failed.");
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                State = TestState.Error;

                _logger.LogError(ex);
                throw;
            }
            finally
            {
                _logger.LogInfo($"End run test `{Name}`");
            }

            stopWatch.Stop();
            return new TestResult(Name, exception, stopWatch.Elapsed);
        }

        public event EventHandler<TestEventArgs>? TestStateChanged;
    }
}
