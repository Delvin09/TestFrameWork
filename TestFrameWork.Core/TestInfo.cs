using System.Reflection;

namespace TestFrameWork.Core
{
    class TestInfo {

        public string Name { get; set; } = string.Empty;
        public MethodInfo? Method { get; set; }

        public void Run(object subject)
        {
            try
            {
                Method?.Invoke(subject, []);
            }
            catch
            {
                //TODO: handle exceptions
            }
        }
    }
}
