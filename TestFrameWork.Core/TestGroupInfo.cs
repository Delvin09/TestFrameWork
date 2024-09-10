using System.Collections.Immutable;
using TestFrameWork.Abstractions.Results;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; init; } = string.Empty;

        public required Type Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; init; }

        public event EventHandler<TestGroupEventArgs>? BeforeGroupTestRun;

        public event EventHandler<TestGroupEventArgs>? AfterGroupTestRun;

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

        public void OnBeforeGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            if (BeforeGroupTestRun != null)
            {
                e = new TestGroupEventArgs { GroupName = Name, FullTypeName = Type.FullName!, AssemblyName = Type.Assembly.FullName! };
                BeforeGroupTestRun?.Invoke(this, e);
            }
        }
        public void OnAfterGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            if (AfterGroupTestRun != null)
            {
                e = new TestGroupEventArgs { GroupName = Name, FullTypeName = Type.FullName!, AssemblyName = Type.Assembly.FullName! };
                AfterGroupTestRun?.Invoke(this, e);
            }
        }
    }
}
