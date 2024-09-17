namespace TestFrameWork.Logging.Abstractions
{
    public interface ILoggerProvider
    {
        ILogger CreateLogger();
        ILoggerProvider Register(Func<ILogger> creator);
        void Clear();
    }
}
