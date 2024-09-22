using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly List<Func<ILogger>> _factories = new List<Func<ILogger>>();

        public void Clear()
        {
            _factories.Clear();
        }

        public ILogger CreateLogger()
        {
            if (_factories.Count == 1)
                return _factories.First()();

            var wrapper = new LoggerWrapper(_factories.Select(f => f()).ToArray());
            return wrapper;
        }

        public ILoggerProvider Register(Func<ILogger> creator)
        {
            _factories.Add(creator);
            return this;
        }
    }
}
