using System.Diagnostics;
using System.Reflection;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    internal class TestInfo
    {
        private readonly string _assemblyName = string.Empty;
        private readonly string _groupName = string.Empty;
        private TestState _testState;
        private string? _message;
        public string Name { get; set; } = string.Empty;

        public MethodInfo? Method { get; set; }

        public event EventHandler<TestEventArgs>? BeforeTest;

        public event EventHandler<AfterTestEventArgs>? AfterTest;
        public TestResult Run(object subject)
        {
            Exception? exception = null;
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                _testState = TestState.Pending;

                TestEventArgs? testArgs = new TestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name };
                OnBeforeTest(testArgs);

                Method?.Invoke(subject, []);

                _testState = TestState.Success;
            }
            
            catch (TargetInvocationException ex) when (ex.InnerException is AssertionFailException)
            {
                _message = ex.InnerException.Message;
                _testState = TestState.Failed;
            }
            catch (Exception ex)
            {
                _testState = TestState.Error;
                _message = ex.Message;
                throw;
            }
            finally
            {
                AfterTestEventArgs afterTestArgs = new AfterTestEventArgs { AssemblyName = _assemblyName, GroupName = _groupName, TestName = Name, Result = _testState, Message = _message };
                OnAfterTest(afterTestArgs);
            }

            stopWatch.Stop();
            return new TestResult(Name, exception, stopWatch.Elapsed);
        }

        public void OnBeforeTest(TestEventArgs e) => 
            BeforeTest?.Invoke(this, e);

        public void OnAfterTest(AfterTestEventArgs e) => 
            AfterTest?.Invoke(this, e);
    }
}
