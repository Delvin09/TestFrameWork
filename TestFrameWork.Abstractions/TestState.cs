namespace TestFrameWork.Abstractions
{
    public enum TestState
    {
        None = 0,
        Pending = 1,
        Running = 2,
        Success = 3,
        Skipped = 4,
        Failed = 5,
        Error = 6
    }
}