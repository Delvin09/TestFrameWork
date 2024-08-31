using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFrameWork.Core.ResultsOfTests;

namespace TestFrameWork.Core
{
    public class TestReport
    {
        private List<TestGroupInfo> tests;
        public TestReport()
        {
            tests = new List<TestGroupInfo>();
        }
        public void AddTestGroup(TestGroupInfo testGroupResult)
        {
            tests.Add(testGroupResult);
        }
        public override string ToString()
        {
            StringBuilder report = new StringBuilder();
            double totalExecutionTime = 0;

            report.AppendLine("Test Report");
            report.AppendLine("-----------");

            foreach (var group in tests)
            {
                report.AppendLine($"Group: {group.Name}");
                if (!group.InstanceSuccess)
                    report.AppendLine("Failed to create instance.");
                else
                {
                    report.AppendLine($"Instance of type {group.Type!.FullName} created successfully.");
                    report.AppendLine($"Group Execution Time: {group.GroupResult.CountTotalTime()}s");
                    totalExecutionTime += group.GroupResult.CountTotalTime();

                    foreach (var test in group.GroupResult._results)
                    {
                        report.Append($"Test: {test.TestName} - ");

                        if (test.Status == "Passed")
                        {
                            report.AppendLine($"Passed - Time: {test.Time}s");
                        }
                        else if (test.Status == "Failed")
                        {
                            report.AppendLine($"Failed - Time: {test.Time}s");
                            report.AppendLine($"Exception: {test.Exception.InnerException!.Message}");
                        }
                        else if (test.Status == "Unhandled Exception")
                        {
                            report.AppendLine($"Unhandled Exception - Time: {test.Time}s");
                            report.AppendLine($"Exception: {test.Exception.Message}");
                        }
                    }

                    report.AppendLine();
                }
            }

            report.AppendLine($"Total Execution Time: {totalExecutionTime}s");
            return report.ToString();
        }
    }
}
