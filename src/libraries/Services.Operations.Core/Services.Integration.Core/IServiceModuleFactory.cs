namespace Services.Integration.Core
{
    public interface IServiceModuleFactory<TModuleConfiguration>
    {
        IServiceModule<TModuleConfiguration> GetServiceModule<TValue>(TModuleConfiguration moduleConfiguration, TValue value);
    }

}
