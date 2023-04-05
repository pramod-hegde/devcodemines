namespace Services.Core.Logging
{
    public interface IProviderConfiguration<TConfig> : IProviderConfiguration
    {
        TConfig Configuration { get; set; }
    }

    public interface IProviderConfiguration
    {
        string ProviderName { get; set; }
    }
}