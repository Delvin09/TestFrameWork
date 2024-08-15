using System.Collections.Immutable;
using System.Reflection;
using TestFrameWork.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestProvider
    {
        private readonly string _assemblyPath;

        public TestProvider(string assemblyPath)
        {
            this._assemblyPath = assemblyPath;
        }

        public IEnumerable<TestGroupInfo> GetTests()
        {
            return Assembly.LoadFile(this._assemblyPath)
                .GetTypes()
                .Where(t => t.GetCustomAttribute<TestGroupAttribute>() != null)
                .Select(t => new TestGroupInfo
                {
                    Name = t.GetCustomAttribute<TestGroupAttribute>()?.Title ?? t.Name,
                    Type = t,
                    Tests = t
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                        .Where(m => m.GetCustomAttribute<TestAttribute>() != null)
                        .Select(m => new TestInfo
                        {
                            Method = m,
                            Name = m.GetCustomAttribute<TestAttribute>()?.Title ?? m.Name,
                        })
                        .ToImmutableArray()
                });
        }
    }
}
