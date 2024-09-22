using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    public static class RegisterExtensions
    {
        public static ILoggerProvider AddConsole(this ILoggerProvider loggerProvider)
        {
            loggerProvider.Register(() => new ConsoleLogger());
            return loggerProvider;
        }
    }
}
