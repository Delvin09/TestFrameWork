using System.Collections.Immutable;
using TestFrameWork.Logging.Abstractions;

namespace TestFrameWork.Core
{
    internal class TestGroupInfo
    {
        private ILogger _logger;

        public TestGroupInfo(ILogger logger)
        {
            _logger = logger;
        }

        public string Name { get; set; } = string.Empty;

        public Type? Type { get; set; }

        public ImmutableArray<TestInfo> Tests { get; set; }

        public void Run()
        {
            _logger.LogInfo($"Start test group `{Name}`. Init test class object.");

            var instance = Activator.CreateInstance(Type!);
            if (instance == null)
            {
                _logger.LogError($"Can't create the test class object for type: `{Type?.FullName}`");
                throw new InvalidOperationException();
            }

            foreach (var test in Tests)
            {
                test.Run(instance);
            }

            _logger.LogInfo($"End test group `{Name}`.");
        }
    }
}
