using System.Collections.Immutable;
using TestFrameWork.Abstractions.Results;
using TestFrameWork.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; init; } = string.Empty;

        public required Type Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; init; }

        public TestGroupResult Run()
        {
            var result = new TestGroupResult(Name);
            try
            {
                if (Type == null) throw new InvalidOperationException($"Test Class [{Name}] can't be found!");

                var instance = Activator.CreateInstance(Type);
                if (instance == null) throw new InvalidOperationException("Test Class isn't created.");

                foreach (var test in Tests)
                {
                    test.BeforeTest += Test_BeforeTest;
                    test.AfterTest += Test_AfterTest;

                    result.AddTestResult(test.Run(instance));

                    test.BeforeTest -= Test_BeforeTest;
                    test.AfterTest -= Test_AfterTest;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        private void Test_AfterTest(object? sender, AfterTestEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Test_BeforeTest(object? sender, TestEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
