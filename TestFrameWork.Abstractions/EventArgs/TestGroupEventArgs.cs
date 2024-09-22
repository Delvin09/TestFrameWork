namespace TestFrameWork.Abstractions.EventArgs
{
    public class TestGroupEventArgs : System.EventArgs
    {
        public required string? GroupName { get; init; }
        public required string? FullTypeName { get; init; }
        public required string? AssemblyName { get; init; }
    }
}