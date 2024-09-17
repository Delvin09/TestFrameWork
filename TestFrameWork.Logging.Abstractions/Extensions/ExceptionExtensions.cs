namespace TestFrameWork.Logging.Abstractions
{
    internal static class ExceptionExtensions
    {
        public static ExceptionInfo ToInfo(
            this Exception ex)
        {
            ArgumentNullException.ThrowIfNull(ex);
            return new ExceptionInfo(ex);
        }
    }
}
