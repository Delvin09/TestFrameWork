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
                        group.Run();
                        testReport.AddTestGroup(group);
                    }
                }
            }
            return testReport;
        }
    }
}
