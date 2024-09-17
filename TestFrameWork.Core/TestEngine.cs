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

        public void Run(string[] assembliesPath)
        {
            foreach (var path in assembliesPath)
            {
                _logger.LogInfo($"Start test for `{path}` assembly.");

                using (var provider = new TestProvider(path, _logger))
                {
                    foreach (var group in provider.GetTests())
                    {
                        group.Run();
                    }
                }

                _logger.LogInfo($"End test for `{path}` assembly.");
            }
        }
    }
}
