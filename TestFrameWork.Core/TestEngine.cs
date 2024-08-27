namespace TestFrameWork.Core
{
    public class TestEngine
    {
        public TestReport ?testReport;
        public void Run(string[] assembliesPath)
        {
            testReport = new TestReport();
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
        }
    }
}
