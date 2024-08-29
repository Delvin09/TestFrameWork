using System;
using System.IO;

namespace TestFrameWork.CLI
{
    public class Logger
    {
        private readonly string fileName;

        public Logger()
        {
            DateTime currentTime = DateTime.Now;
            fileName = $"log_{currentTime.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogError(string message, Exception ex = null)
        {
            if (ex != null)
            {
                Log("ERROR", $"{message}. Exception: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
            else
            {
                Log("ERROR", message);
            }
        }

        private void Log(string logType, string message)
        {
            DateTime logTime = DateTime.Now;
            string logMessage = $"{logTime.ToString("yyyy-MM-dd HH:mm:ss")} [{logType}] {message}";

            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(logMessage);
            }
        }
    }
}
