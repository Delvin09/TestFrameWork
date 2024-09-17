using System.Reflection;
using TestFrameWork.Abstractions;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    class TestInfo
    {
        private ILogger _logger;

        public TestInfo(ILogger logger)
        {
            _logger = logger;
        }

        public string Name { get; set; } = string.Empty;

        public MethodInfo? Method { get; set; }

        public void Run(object subject)
        {
            _logger.LogInfo($"Run test `{Name}`");
            try
            {
                Method?.Invoke(subject, []);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is AssertionFailException)
            {
                _logger.LogWarning($"Test `{Name}` assertion failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Test `{Name}` failed with exception.", ex);
            }
            finally
            {
                _logger.LogInfo($"End run test `{Name}`");
            }
        }
    }
}
