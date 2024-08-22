namespace TestFrameWork.Abstractions
{
    public class AfterTestEventArgs : TestEventArgs
    {
        public string? Message;
        public TestState Result { get; init; }
    }
}