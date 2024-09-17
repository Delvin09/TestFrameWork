using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    public struct LogInfo
    {
        public DateTime DateTime;
        public string? Message;
        public LogType Type;
        public ExceptionInfo? Exception;
    }
}
