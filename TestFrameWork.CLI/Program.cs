using TestFrameWork.Core;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new TestEngine();
            engine.Run(args);

            SaveLogToFile logger = new SaveLogToFile();
            logger.SaveLog();
        }
    }
}
