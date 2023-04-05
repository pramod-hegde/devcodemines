using Microsoft.ApplicationInsights.DataContracts;
using System;

namespace Services.Core.Logging
{
    public class TraceTelemetryLog
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
        public string Sequence { get; set; }
        public SeverityLevel? SeverityLevel { get; set; }

        public static implicit operator TraceTelemetryLog(string value) 
        {
            return new TraceTelemetryLog { Message = value, Timestamp = new DateTimeOffset(DateTime.UtcNow) };
        }
    }
}