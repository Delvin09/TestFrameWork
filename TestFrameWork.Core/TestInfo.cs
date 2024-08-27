using System.Reflection;
using TestFrameWork.Abstractions;

namespace TestFrameWork.Core
{
    public class TestInfo
    {
        public string Name { get; set; } = string.Empty;
        public MethodInfo? Method { get; set; }
        public Exception Run(object subject)
        {
            try
            {
                Method?.Invoke(subject, []);
                return null!;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
