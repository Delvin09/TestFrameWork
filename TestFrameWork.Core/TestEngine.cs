using System.Xml.Linq;

namespace TestFrameWork.Core
{
    public class TestEngine
    {
        public void Run(string[] assembliesPath)
        {
            foreach (var path in assembliesPath)
            {
                using (var provider = new TestProvider(path))
                {
                    foreach (var group in provider.GetTests())
                    {
                        group.BeforeGroupTestRun += OnBeforeGroupTestRun;
                        group.Run();
                        group.AfterGroupTestRun += OnAfterGroupTestRun;

                        group.BeforeGroupTestRun -= OnBeforeGroupTestRun;
                        group.AfterGroupTestRun -= OnAfterGroupTestRun;
                    }
                }
            }
        }

        private void OnBeforeGroupTestRun(object? sender, TestGroupEventArgs e) => OnBeforeGroupTestRun(sender, e);

        private void OnAfterGroupTestRun(object? sender, TestGroupEventArgs e) => OnAfterGroupTestRun(sender, e);
    }
}
