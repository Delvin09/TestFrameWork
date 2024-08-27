﻿using System.Collections.Immutable;
using System.Diagnostics;

namespace TestFrameWork.Core
{
    public class TestGroupInfo
    {
        public string Name { get; set; } = string.Empty;
        public Type? Type { get; set; }
        public ImmutableArray<TestInfo> Tests { get; set; }
        public TestGroupResult GroupResult { get; set; }
        public TestGroupInfo()
        {
            GroupResult = new TestGroupResult();
        }
        public void Run()
        {
            var instance = Activator.CreateInstance(Type);
            if (instance == null) throw new InvalidOperationException();
            foreach (var test in Tests)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                Exception success = test.Run(instance);
                stopWatch.Stop();
                double time = stopWatch.Elapsed.TotalSeconds;
                GroupResult.AddTestResult(new TestResult(test.Name, success, time));
            }
        }
    }
}
