namespace Services.Core.Logging
{
    public interface IConcreateLoggerProvider
    {
        ILogger[] Loggers { get; }
    }
}