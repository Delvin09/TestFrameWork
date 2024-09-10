using System.Diagnostics;
using System.Reflection;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    internal class TestInfo
    {
        public string Name { get; set; } = string.Empty;

        public MethodInfo? Method { get; set; }

        public TestResult Run(object subject)
        {
            Exception? exception = null;
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                Method?.Invoke(subject, []);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            stopWatch.Stop();
            return new TestResult(Name, exception, stopWatch.Elapsed);
        }
    }
}
