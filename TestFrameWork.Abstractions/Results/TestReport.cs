using System.Text;

namespace TestFrameWork.Abstractions.Results
{
    public class TestReport
    {
        private List<TestGroupResult> _testGroupResults = new List<TestGroupResult>();

        public void AddTestGroup(TestGroupResult testGroupResult)
        {
            _testGroupResults.Add(testGroupResult);
        }

        public override string ToString()
        {
            StringBuilder report = new StringBuilder();
            double totalExecutionTime = 0;

            report.AppendLine("Test Report");
            report.AppendLine("-----------");

            foreach (var group in _testGroupResults)
            {
                var groupTotalCount = group.CountTotalTime();
                totalExecutionTime += groupTotalCount;

                report.AppendLine($"Group: {group.Name}");
                report.AppendLine($"Group Execution Time: {groupTotalCount}s");

                foreach (var test in group)
                {
                    report.Append($"Test: {test.TestName} - ");

                    if (test.Status == "Passed")
                    {
                        report.AppendLine($"Passed - Time: {test.Time}s");
                    }
                    else if (test.Status == "Failed")
                    {
                        report.AppendLine($"Failed - Time: {test.Time}s");
                        report.AppendLine($"Exception: {test.Exception?.InnerException!.Message}");
                    }
                    else if (test.Status == "Unhandled Exception")
                    {
                        report.AppendLine($"Unhandled Exception - Time: {test.Time}s");
                        report.AppendLine($"Exception: {test.Exception?.Message}");
                    }
                }

                report.AppendLine();
            }

            report.AppendLine($"Total Execution Time: {totalExecutionTime}s");
            return report.ToString();
        }

        public void SaveToFile(string? path = null)
        {
            string filePath = string.IsNullOrEmpty(path) ? "testResults.txt" : path;

            string ext = string.IsNullOrEmpty(Path.GetExtension(filePath)) ? ".txt" : Path.GetExtension(filePath);
            string? fileName = Path.GetFileNameWithoutExtension(filePath);
            string? folderPath = Path.GetDirectoryName(filePath) ?? string.Empty;

            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException(nameof(fileName));

            path = Path.Combine(folderPath, $"{fileName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss.ffff}{ext}");

            try
            {
                File.WriteAllText(filePath, ToString());
                Console.WriteLine($"Report successfully written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write report to file. Exception: {ex.Message}");
            }
        }
    }
}
