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
                        group.BeforeGroupTestRun += Group_BeforeGroupTestRun;
                        group.AfterGroupTestRun += Group_AfterGroupTestRun;

                        var groupTestResult = group.Run();
                        testReport.AddTestGroup(groupTestResult);


                        group.BeforeGroupTestRun -= Group_BeforeGroupTestRun;
                        group.AfterGroupTestRun -= Group_AfterGroupTestRun;
                    }
                }
            }
            return testReport;
        }

        private void Group_BeforeGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Group_AfterGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
