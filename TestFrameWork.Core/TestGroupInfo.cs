using System.Collections.Immutable;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; set; } = string.Empty;

        public Type Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; set; }

        public void Run()
        {
            var instance = Activator.CreateInstance(Type);
            if (instance == null) throw new InvalidOperationException();

            foreach (var test in Tests)
            {
                test.Run(instance);
            }
        }
    }
}
