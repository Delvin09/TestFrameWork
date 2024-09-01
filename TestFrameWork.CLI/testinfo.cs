using System.Reflection;

namespace TestFrameWork.Core
{
    class TestInfo
    {
        public string Name { get; set; } = string.Empty;
        public MethodInfo? Method { get; set; }

        public void Run(object subject)
        {
            try
            {
                Method?.Invoke(subject, Array.Empty<object>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in test {Name}: {ex.Message}");
                throw;
            }
        }
    }
}
