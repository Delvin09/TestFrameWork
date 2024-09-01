using System.Reflection;

namespace TestFrameWork.Core
{
    public class TestResult
    {
        public string TestName {  get; init; }

        public Exception? Exception { get; init; }

        public string? Status { get; private set; }

        public double Time { get; private set; }

        public TestResult(string testName, Exception? success, TimeSpan time)
        {
            TestName = testName;
            Exception = success;
            GetTime(time);
            GetStatus();
        }

        private void GetStatus()
        {
            if (Exception == null)
                Status = "Passed";
            else if (Exception is TargetInvocationException)
                Status = "Failed";
            else
                Status = "Unhandled Exception";
        }

        private void GetTime(TimeSpan timeSpan)
        {
            Time = timeSpan.TotalSeconds;
        }
    }
}
