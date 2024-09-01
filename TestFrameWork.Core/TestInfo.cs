﻿using System.Diagnostics;
using System.Reflection;
using TestFrameWork.Core.ResultsOfTests;
using TestFrameWork.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace TestFrameWork.Core
{
    public class TestInfo
    {
        public string Name { get; set; } = string.Empty;
        public MethodInfo? Method { get; set; }
        public TestResult Run(object subject)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                Method?.Invoke(subject, []);
                stopWatch.Stop();
                return new TestResult(Name, null!, stopWatch.Elapsed);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                return new TestResult(Name, ex, stopWatch.Elapsed);
            }
        }
    }
}
