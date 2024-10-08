﻿using TestFrameWork.Abstractions.Results;
using TestFrameWork.Core;
using TestFrameWork.Logging;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loggerProvider = new LoggerProvider();
            var logger = loggerProvider
                .AddConsole()
                .AddFile()
                .CreateLogger();

            try
            {
                logger.LogInfo("Test engine starts.");

                var engine = new TestEngine(logger);
                TestReport testReport = engine.Run(args);
                testReport.SaveToFile();
            }
            catch (Exception ex)
            {
                logger.LogError("Unhandled exception was occurred.", ex);
                throw;
            }
        }
    }
}
