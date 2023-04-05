namespace Services.Core.Logging
{
    [LoggerProvider("Debug")]
    public class DebugLoggingProvider : ILoggerProvider
    {
        internal IDebugLogConfiguration _config;

        public DebugLoggingProvider(IDebugLogConfiguration config)
        {
            _config = config;
        }

        ILogger ILoggerProvider.CreateLogger()
        {
            if (_config.Disabled)
            {
                return default;
            }
            return new DebugLogger(_config);
        }
    }
}