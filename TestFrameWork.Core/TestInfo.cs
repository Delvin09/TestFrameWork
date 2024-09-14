using System.Diagnostics;
using System.Reflection;
using TestFrameWork.Abstractions;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    internal class TestInfo
    {
        private readonly string _assemblyName = string.Empty;
        private readonly string _groupName = string.Empty;

        private TestState _testState = TestState.Pending;
        private string? _message;

        public string Name { get; set; } = string.Empty;

        public MethodInfo? Method { get; set; }

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
                // TODO: log exception
            }
        }

        public TestResult Run(object subject)
        {
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
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                State = TestState.Error;
                throw;
            }

            stopWatch.Stop();
            return new TestResult(Name, exception, stopWatch.Elapsed);
        }

        public event EventHandler<TestEventArgs>? TestStateChanged;
    }
}
