namespace TestFrameWork.Abstractions
{
    public class TestEventArgs : EventArgs
    {
        public required string AssemblyName { get; init; }

        public required string GroupName { get; init; }

        public required string TestName { get; init; }

        public string? Message { get; init; } = null;

        public TestState NewState { get; init; }

        public TestState OldState { get; init; }
    }
}