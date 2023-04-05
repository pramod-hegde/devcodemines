namespace Services.Core.Logging
{
    public interface ILogger<out TContext> : ILogger
    {
        ILogger<TContext> AddProvider<TConfig>(IProviderConfiguration<TConfig> configuration);
        void Setup();
    }
}