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
                        group.Run();
                    }
                }
            }
        }
    }
}
