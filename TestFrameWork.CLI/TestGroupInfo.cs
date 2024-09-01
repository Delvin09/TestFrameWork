using System.Collections.Immutable;
using System.Reflection;
using System.Linq;
using System;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        public string Name { get; set; } = string.Empty;
        public Type Type { get; set; }
        public ImmutableArray<TestInfo> Tests { get; set; }

        public void Run()
        {
            var instance = Activator.CreateInstance(Type);
            if (instance == null) throw new InvalidOperationException();

            var initMethod = Type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(m => m.GetCustomAttribute<InitializationAttribute>() != null);

            if (initMethod != null)
            {
                try
                {
                    Console.WriteLine($"Running initialization for {Name}...");
                    initMethod.Invoke(instance, Array.Empty<object>());
                    Console.WriteLine("Initialization completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Initialization failed: {ex.Message}");
                    return;
                }
            }

            foreach (var test in Tests)
            {
                try
                {
                    Console.WriteLine($"Running test: {test.Name}...");
                    test.Run(instance);
                    Console.WriteLine($"Test {test.Name}: PASSED");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test {test.Name}: FAILED - {ex.Message}");
                    return;
                }
            }
        }
    }
}