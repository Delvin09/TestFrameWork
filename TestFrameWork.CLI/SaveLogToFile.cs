using System;
using System.IO;

namespace TestFrameWork.CLI
{
    public class SaveLogToFile
    {
        public void SaveLog()
        {
            DateTime currentTime = DateTime.Now;

            string fileName = $"log_{currentTime.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";

            using (StreamWriter writer = new StreamWriter(File.Create(fileName)))
            {
                writer.WriteLine("Час запуску програми: " + currentTime);

                writer.WriteLine("Логування запущено успішно");
            }

            Console.WriteLine("Лог записано у файл: " + fileName);
        }
    }
}
