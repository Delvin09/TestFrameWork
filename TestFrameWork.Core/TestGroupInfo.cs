using System.Collections.Immutable;
using System.Reflection;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; internal set; } = string.Empty;

        public Type Type { get; internal set; }

        public ImmutableArray<TestInfo> Tests { get; internal set; }

        public MethodInfo? Initializer { get; internal set; }

        public void Run()
        {
            var instance = Activator.CreateInstance(Type);
            if (instance == null) throw new InvalidOperationException();

            try
            {
                Initializer?.Invoke(instance, null);
                foreach (var test in Tests)
                {
                    test.Run(instance);
                }
            }
            catch (Exception ex)
            {
                // TODO: log error
            }
        }
    }
}
