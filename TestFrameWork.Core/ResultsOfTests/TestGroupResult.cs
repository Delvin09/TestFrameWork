using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrameWork.Core.ResultsOfTests
{
    public class TestGroupResult
    {
        public List<TestResult> _results;
        double _totalGroupTime { get => CountTotalTime(); }
        public TestGroupResult()
        {
            _results = new List<TestResult>();
        }
        public double CountTotalTime()
        {
            double total = 0;
            foreach (var result in _results)
            {
                total += result.Time;
            }
            return total;
        }
        public void AddTestResult(TestResult testResult)
        {
            _results.Add(testResult);
        }
    }
}
