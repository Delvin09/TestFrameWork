namespace TestFrameWork.Abstractions
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class TestAttribute : Attribute
    {
        public string? Title { get; set; }
    }
}
