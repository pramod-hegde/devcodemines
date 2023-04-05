namespace Services.Core.Logging
{
    [LoggerProvider("ApplicationInsights")]
    public class ApplicationInsightsProvider : ILoggerProvider
    {
        internal IApplicationInsightsLogConfiguration _config;

        public ApplicationInsightsProvider(IApplicationInsightsLogConfiguration config)
        {
            _config = config;
        }

        ILogger ILoggerProvider.CreateLogger()
        {
            if (_config.Disabled)
            {
                return default;
            }
            return new ApplicationInsightsLogger(_config);
        }
    }
}