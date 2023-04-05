using System.Collections.Generic;

namespace Services.Core.Logging
{
    public interface ILoggerContext
    {
        string CorrelationId { get; set; }
        string SessionId { get; set; }
        string ExecutionContext { get; set; }
        IDictionary<string, object> CommonProperties { get; set; }
    }
}