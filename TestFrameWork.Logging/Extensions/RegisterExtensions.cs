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

        public static ILoggerProvider AddFile(this ILoggerProvider loggerProvider)
        {
            loggerProvider.Register(() => new FileLogger());
            return loggerProvider;
        }
    }
}
