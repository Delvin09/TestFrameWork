namespace TestFrameWork.Core
{
    public class TestGroupEventArgs : EventArgs
    {
        public required string GroupName { get; init; }
        public required string FullTypeName { get; init; }
        public required string AssemblyName { get; init; }
    }
}