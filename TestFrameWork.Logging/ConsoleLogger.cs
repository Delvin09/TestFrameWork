using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Logging
{
    public class ConsoleLogger : ILogger
    {
        private readonly object _lock = new object();

        public void Log(LogInfo data)
        {
            lock (_lock)
            {
                Console.ResetColor();
                WriteLogType(data.Type);
                Console.Write("\t");
                WriteLogTime(data.DateTime);
                Console.WriteLine();
                Console.Write("\t");
                WriteMessage(data.Message!);
                Console.WriteLine();
                Console.Write("\t");
                WriteMessage(data.Exception?.ToString()!);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private void WriteMessage(string message)
        {
            Console.ResetColor();
            Console.Write(message);
        }

        private void WriteLogTime(DateTime logTime)
        {
            Console.ResetColor();
            Console.Write($"|");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{logTime:yyyy-MM-dd HH:mm:ss}");
            Console.ResetColor();
            Console.Write("|");
        }

        private void WriteLogType(LogType logType)
        {
            Console.ResetColor();
            Console.Write("[");

            switch (logType)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("INFO");
                    break;
                case LogType.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("WARN");
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERROR");
                    break;
                default:
                    goto case LogType.Info;
            }

            Console.ResetColor();
            Console.Write("]:");
        }
    }
}
