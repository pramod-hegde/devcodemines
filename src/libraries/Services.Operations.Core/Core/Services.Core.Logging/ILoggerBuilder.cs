namespace Services.Core.Logging
{
    public interface ILoggerBuilder
    {
        ILoggerBuilder Add<TConfig>(IProviderConfiguration<TConfig> providerConfig);
        IConcreateLoggerProvider Build();
    }
}