using System;

namespace Services.Core.Logging
{
    public class DependencyTelemetryLog
    {
        public string Data { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public bool? Success { get; set; }
        public string ResultCode { get; set; }
    }
}