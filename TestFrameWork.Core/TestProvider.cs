using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.Loader;
using TestFrameWork.Abstractions;
using TestFrameWork.Core.ResultsOfTests;

namespace TestFrameWork.Core
{
    internal class TestProvider : IDisposable
    {
        private readonly string _assemblyPath;
        private readonly AssemblyLoadContext _context;

        public TestProvider(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
            _context = new AssemblyLoadContext(_assemblyPath, true);
            _context.Resolving += _context_Resolving;
        }

        private Assembly? _context_Resolving(AssemblyLoadContext ctx, AssemblyName assemblyToLoad)
        {
            var dir = Path.GetDirectoryName(_assemblyPath);

            var assemblyPath = Path.Combine(dir, assemblyToLoad.Name += ".dll");

            var assembly = ctx.LoadFromAssemblyPath(assemblyPath);
            return assembly;
        }

        public void Dispose()
        {
            try
            {
                _context.Resolving -= _context_Resolving;
                _context.Unload();
            }
            catch
            {
            }
        }

        public IEnumerable<TestGroupInfo> GetTests()
        {
            return _context.LoadFromAssemblyPath(_assemblyPath)
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
