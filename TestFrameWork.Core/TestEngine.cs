using TestFrameWork.Abstractions.EventArgs;
using TestFrameWork.Abstractions.Results;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    public class TestEngine
    {
        private readonly ILogger _logger;

        public TestEngine(ILogger logger)
        {
            _logger = logger;
        }

        public TestReport Run(string[] assembliesPath)
        {
            TestReport testReport = new TestReport();

            foreach (var path in assembliesPath)
            {
                _logger.LogInfo($"Start test for `{path}` assembly.");

                using (var provider = new TestProvider(path, _logger))
                {
                    foreach (var group in provider.GetTests())
                    {
                        OnBeforeGroupTestRun(group);

                        group.BeforeTestRun += Group_BeforeTestRun;
                        group.AfterTestRun += Group_AfterTestRun;

                        try
                        {
                            var groupTestResult = group.Run();
                            testReport.AddTestGroup(groupTestResult);
                        }
                        finally
                        {
                            group.BeforeTestRun -= Group_BeforeTestRun;
                            group.AfterTestRun -= Group_AfterTestRun;
                        }

                        OnAfterGroupTestRun(group);
                    }
                }

                _logger.LogInfo($"End test for `{path}` assembly.");
            }
            return testReport;
        }

        private void OnBeforeGroupTestRun(TestGroupInfo? testGroup)
        {
            try
            {
                if (testGroup == null) return;

                BeforeGroupTestRun?.Invoke(
                    this,
                    new TestGroupEventArgs
                    {
                        GroupName = testGroup.Name,
                        FullTypeName = testGroup.Type?.FullName,
                        AssemblyName = testGroup.Type?.Assembly?.FullName
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        private void OnAfterGroupTestRun(TestGroupInfo? testGroup)
        {
            try
            {
                if (testGroup == null) return;

                AfterGroupTestRun?.Invoke(
                    this,
                    new TestGroupEventArgs
                    {
                        GroupName = testGroup.Name,
                        FullTypeName = testGroup.Type?.FullName,
                        AssemblyName = testGroup.Type?.Assembly?.FullName
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        private void Group_AfterTestRun(object? sender, TestEventArgs e) => AfterTestRun?.Invoke(this, e);

        private void Group_BeforeTestRun(object? sender, TestEventArgs e) => BeforeTestRun?.Invoke(this, e);

        public event EventHandler<TestEventArgs>? BeforeTestRun;
        public event EventHandler<TestEventArgs>? AfterTestRun;
        public event EventHandler<TestGroupEventArgs>? BeforeGroupTestRun;
        public event EventHandler<TestGroupEventArgs>? AfterGroupTestRun;
    }
}
