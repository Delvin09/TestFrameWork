using TestFrameWork.Core;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new TestEngine();
            TestReport testReport = engine.Run(args);
            testReport.WriteToTxtFile();
        }
    }
}
