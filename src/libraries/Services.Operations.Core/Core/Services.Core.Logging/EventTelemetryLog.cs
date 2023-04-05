using System;

namespace Services.Core.Logging
{
    public class EventTelemetryLog
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Sequence { get; set; }
        public string Name { get; set; }

        public static implicit operator EventTelemetryLog(string value) 
        {
            return new EventTelemetryLog { Name = value, Timestamp = new DateTimeOffset(DateTime.UtcNow) };
        }
    }
}