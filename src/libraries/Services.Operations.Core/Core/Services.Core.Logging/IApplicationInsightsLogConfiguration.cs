using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Caching.Memory;

namespace Services.Core.Logging
{
    public interface IApplicationInsightsLogConfiguration
    {
        string InstrumentationKey { get; set; }       
        bool Disabled { get; set; }
        bool EnableInitializers { get; set; }
        bool DeveloperMode { get; set; }
        IMemoryCache Cache { get; set; }
        TelemetryConfiguration ActiveTelemetryConfiguration { get; set; }
    }
}