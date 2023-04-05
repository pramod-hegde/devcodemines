namespace Services.Core.Logging
{
    public interface ILoggerProvider
    {
        ILogger CreateLogger();
    }
}