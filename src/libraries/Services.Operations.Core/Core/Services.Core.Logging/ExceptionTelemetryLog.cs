using Microsoft.ApplicationInsights.DataContracts;
using System;

namespace Services.Core.Logging
{
    public class ExceptionTelemetryLog
    {
        public Exception Exception { get; set; }
        public SeverityLevel? SeverityLevel { get; set; }
        public string Message { get; set; }
        public string Sequence { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ProblemId { get; set; }
    }
}