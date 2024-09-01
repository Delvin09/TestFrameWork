using System.Collections;

namespace TestFrameWork.Core
{
    public class TestGroupResult : IEnumerable<TestResult>
    {
        private List<TestResult> _testResults = new List<TestResult>();

        public double TotalTime => CountTotalTime();

        public Exception? Exception { get; set; }

        public string Name { get; } = string.Empty;

        public TestGroupResult(string name)
        {
            Name = name;
        }

        public double CountTotalTime()
        {
            double total = 0;
            foreach (var result in _testResults)
            {
                total += result.Time;
            }
            return total;
        }

        public void AddTestResult(TestResult testResult)
        {
            _testResults.Add(testResult);
        }

        public IEnumerator<TestResult> GetEnumerator()
        {
            return _testResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
