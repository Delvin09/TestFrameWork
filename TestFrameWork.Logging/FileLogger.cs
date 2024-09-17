using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _fileName;
        private readonly object _syncFile = new object();

        public FileLogger()
        {
            DateTime currentTime = DateTime.Now;
            _fileName = $"log_{currentTime.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
        }

        public void Log(LogInfo data)
        {
            lock (_syncFile)
            {
                File.AppendAllLines(_fileName, [data.ToString()!]);
            }
        }
    }
}
