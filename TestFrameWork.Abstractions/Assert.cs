namespace TestFrameWork.Abstractions
{
    public static class Assert
    {
        public static void AreEquals<T>(T actual, T expected, string? msg = null)
        {
            if (!object.Equals(actual, expected))
                throw new AssertionFailException(msg ?? "Actual not equal expected.");
        }

        public static void AreNotEquals<T>(T actual, T expected, string? msg = null)
        {
            if (object.Equals(actual, expected))
                throw new AssertionFailException(msg ?? "Actual equal expected.");
        }

        public static void IsTrue(bool condition, string? msg = null)
        {
            if (!condition)
                throw new AssertionFailException(msg ?? "Condition is false.");
        }

        public static void IsFalse(bool condition, string? msg = null)
        {
            if (condition)
                throw new AssertionFailException(msg ?? "Condition is true.");
        }
    }
}
