namespace TestFrameWork.Abstractions
{
    public class AssertionFailException : Exception
    {
        public AssertionFailException(string message) : base(message) { }
        public AssertionFailException(string message, Exception inner) : base(message, inner) { }
    }
}
