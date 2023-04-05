namespace Services.Core.Logging
{
    public interface ILoggerFactory
    {
        ILoggerBuilder DefaultBuilder { get; }
    }
}