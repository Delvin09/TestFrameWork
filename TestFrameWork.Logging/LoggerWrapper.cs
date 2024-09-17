using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    internal class LoggerWrapper : ILogger
    {
        private readonly HashSet<ILogger> _loggers = new HashSet<ILogger>();

        public LoggerWrapper(ILogger[] loggers)
        {
            for (int i = 0; i < loggers.Length; i++)
                _loggers.Add(loggers[i]);
        }

        public void Log(LogInfo data)
        {
            foreach (var l in _loggers)
                l.Log(data);
        }
    }
}
