using System.Collections.Immutable;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; set; } = string.Empty;

        public required Type Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; set; }

        public event EventHandler<TestGroupEventArgs>? BeforeGroupTestRun;

        public event EventHandler<TestGroupEventArgs>? AfterGroupTestRun;

        public void Run()
        {
            var instance = Activator.CreateInstance(Type);

            if (instance == null) 
                throw new InvalidOperationException();

            BeforeGroupTestRun += OnBeforeGroupTestRun;

            foreach (var test in Tests)
            {
                test.Run(instance);
            }

            AfterGroupTestRun += OnAfterGroupTestRun;

            BeforeGroupTestRun -= OnBeforeGroupTestRun;
            AfterGroupTestRun -= OnAfterGroupTestRun;
        }

        private void OnBeforeGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            if (BeforeGroupTestRun != null)
            {
                e = new TestGroupEventArgs { GroupName = Name };
                BeforeGroupTestRun?.Invoke(this, e);
            }
        }
        private void OnAfterGroupTestRun(object? sender, TestGroupEventArgs e)
        {
            if (AfterGroupTestRun != null)
            {
                e = new TestGroupEventArgs { GroupName = Name };
                AfterGroupTestRun?.Invoke(this, e);
            }
        }
    }
}
