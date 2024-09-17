using TestFrameWork.Core;
using TestFrameWork.Logging;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loggerProvider = new LoggerProvider();
            var logger = loggerProvider
                .AddConsole()
                .CreateLogger();

            try
            {
                logger.LogInfo("Test engine starts.");

                var engine = new TestEngine(logger);
                engine.Run(args);
            }
            catch (Exception ex)
            {
                logger.LogError("Unhandled exception was occurred.", ex);
                throw;
            }
        }
    }
}
