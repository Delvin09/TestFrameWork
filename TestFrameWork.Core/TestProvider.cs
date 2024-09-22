using System.Collections.Immutable;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Loader;
using TestFrameWork.Abstractions;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestProvider : IDisposable
    {
        private readonly string _assemblyPath;
        private readonly ILogger _logger;
        private readonly AssemblyLoadContext _context;

        public TestProvider(string assemblyPath, ILogger logger)
        {
            _assemblyPath = assemblyPath;
            _logger = logger;
            _context = new AssemblyLoadContext(_assemblyPath, true);
            _context.Resolving += _context_Resolving;
        }

        private Assembly? _context_Resolving(AssemblyLoadContext ctx, AssemblyName assemblyToLoad)
        {
            _logger.LogInfo($"Try to resolve dependent assembly: `{assemblyToLoad.Name}`");
            try
            {
                var dir = Path.GetDirectoryName(_assemblyPath);

            var assemblyPath = Path.Combine(dir!, assemblyToLoad.Name += ".dll");

                var assembly = ctx.LoadFromAssemblyPath(assemblyPath);
                return assembly;
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't resolve dependent assembly. Exception occurred.", ex);
                throw;
            }
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
                .Select(t => new TestGroupInfo(_logger)
                {
                    Name = t.GetCustomAttribute<TestGroupAttribute>()?.Title ?? t.Name,
                    Type = t,
                    Initializer = t
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                        .FirstOrDefault(m => m.GetCustomAttribute<InitializationAttribute>() != null),
                    Tests = t
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                        .Where(m => m.GetCustomAttribute<TestAttribute>() != null)
                        .Select(m => new TestInfo(_logger)
                        {
                            Method = m,
                            Name = m.GetCustomAttribute<TestAttribute>()?.Title ?? m.Name,
                        })
                        .ToImmutableArray()
                });
        }
    }
}
