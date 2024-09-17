namespace TestFrameWork.Logging.Abstractions
{
    public static class LoggerExtensions
    {
        public static void LogInfo(this ILogger logger, string message)
            => logger.Log(LogType.Info, message);

        public static void LogWarning(this ILogger logger, string message)
            => logger.Log(LogType.Warn, message);

        public static void LogError(this ILogger logger, string message, Exception? exception = null)
            => logger.Log(LogType.Error, message, exception);

        public static void Log(this ILogger logger, LogType logType, string message, Exception? exception = null)
            => logger.Log(new LogInfo { DateTime = DateTime.Now, Message = message, Type = logType, Exception = exception?.ToInfo() });
    }
}
