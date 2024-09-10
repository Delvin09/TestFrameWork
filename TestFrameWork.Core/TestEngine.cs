using System.Xml.Linq;

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
                        group.BeforeGroupTestRun += OnBeforeGroupTestRun;

                        var groupTestResult = group.Run();
                        testReport.AddTestGroup(groupTestResult);

                        group.AfterGroupTestRun += OnAfterGroupTestRun;

                        group.BeforeGroupTestRun -= OnBeforeGroupTestRun;
                        group.AfterGroupTestRun -= OnAfterGroupTestRun;
                    }
                }
            }
            return testReport;
        }

        private void OnBeforeGroupTestRun(object? sender, TestGroupEventArgs e) => OnBeforeGroupTestRun(sender, e);

        private void OnAfterGroupTestRun(object? sender, TestGroupEventArgs e) => OnAfterGroupTestRun(sender, e);
    }
}
