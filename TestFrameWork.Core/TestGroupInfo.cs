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
                    test.BeforeTest += OnBeforeTest;
                    test.AfterTest += OnAfterTest;

                    result.AddTestResult(test.Run(instance));

                    test.BeforeTest -= OnBeforeTest;
                    test.AfterTest -= OnAfterTest;
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;

            }

            return result;
        }

        public void OnBeforeTest(object? sender, TestEventArgs e)
        {
            //TODO: Handle OnBeforeTest event
        }

        public void OnAfterTest(object? sender, AfterTestEventArgs e)
        {
            //TODO: Handle OnAfterTest event
        }
    }
}
