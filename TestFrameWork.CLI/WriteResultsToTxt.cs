using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestFrameWork.Core;

namespace TestFrameWork.CLI
{
    public static class WriteResultToTxt
    {
        public static void WriteResultToFile(TestReport testReport)
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(directoryPath, "TestReport.txt");
            try
            {
                File.WriteAllText(filePath, testReport.ToString());
                Console.WriteLine($"Report successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write report to file. Exception: {ex.Message}");
            }
        }
    }
}