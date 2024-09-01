using System.Collections.Immutable;
using TestFrameWork.Core.ResultsOfTests;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; init; } = string.Empty;

        public Type? Type { get; init; }

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
                    result.AddTestResult(test.Run(instance));
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }
    }
}
