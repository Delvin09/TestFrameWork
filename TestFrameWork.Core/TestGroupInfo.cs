using System.Collections.Immutable;
using TestFrameWork.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; set; } = string.Empty;

        public required Type Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; set; }
        public void Run()
        {
            var instance = Activator.CreateInstance(Type);
            if (instance == null) throw new InvalidOperationException();

            foreach (var test in Tests)
            {
                test.BeforeTest += OnBeforeTest;
                test.AfterTest += OnAfterTest;

                test.Run(instance);

                test.BeforeTest -= OnBeforeTest;
                test.AfterTest -= OnAfterTest;
            }
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
