using System.Reflection;
using TestFrameWork.Abstractions;

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
                Method?.Invoke(subject, []);
            }
            //TODO: handle exceptions
            catch (TargetInvocationException ex) when (ex.InnerException is AssertionFailException)
            {
                // Тест просто свалився
            }
            catch (Exception ex)
            {

            }
        }
    }
}
