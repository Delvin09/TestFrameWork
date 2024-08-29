using TestFrameWork.Core;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new TestEngine();
            engine.Run(args);


              Logger logger = new Logger();

            try
            {
                logger.LogInfo("Програма запущена успішно.");

                throw new InvalidOperationException("Тестова помилка");
            }
            catch (Exception ex)
            {
                logger.LogError("Виникла помилка під час виконання програми", ex);
            }
        }
    }
}
