using TestFrameWork.Core;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = ne w TestEngine();
            engine.Run(args);
        }
    }
}
