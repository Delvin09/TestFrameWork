using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestFrameWork.Logging
{
    public class ExceptionInfo
    {
        private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
        };

        public ExceptionInfo() { }

        internal ExceptionInfo(Exception exception, bool includeInnerException = true, bool includeStackTrace = false)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Type = exception.GetType().FullName;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = includeStackTrace ? exception.StackTrace : null;
            if (includeInnerException && exception.InnerException is not null)
            {
                InnerException = new ExceptionInfo(exception.InnerException, includeInnerException, includeStackTrace);
            }
        }

        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public string? StackTrace { get; set; }
        public ExceptionInfo? InnerException { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _defaultJsonSerializerOptions);
        }
    }
}
