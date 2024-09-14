using TestFrameWork.Abstractions;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    public class TestEngine
    {
        public TestReport Run(string[] assembliesPath)
        {
            TestReport testReport = new TestReport();

            foreach (var path in assembliesPath)
            {
                using (var provider = new TestProvider(path))
                {
                    foreach (var group in provider.GetTests())
                    {
                        group.BeforeTestRun += Group_BeforeTestRun;

                        try
                        {
                            var groupTestResult = group.Run();
                            testReport.AddTestGroup(groupTestResult);
                        }
                        finally
                        {
                            group.AfterTestRun += Group_AfterTestRun;
                        }
                    }
                }
            }
            return testReport;
        }

        private void Group_AfterTestRun(object? sender, TestEventArgs e) => AfterTestRun?.Invoke(this, e);

        private void Group_BeforeTestRun(object? sender, TestEventArgs e) => BeforeTestRun?.Invoke(this, e);

        public event EventHandler<TestEventArgs>? BeforeTestRun;
        public event EventHandler<TestEventArgs>? AfterTestRun;
    }
}
