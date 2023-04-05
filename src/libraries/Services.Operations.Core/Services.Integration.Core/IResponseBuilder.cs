namespace Services.Integration.Core
{
    public interface IResponseBuilder
    {
        object CreateResponse<TIn>(TIn input, object externalServiceResponse);
        void SetConfig<TConfig>(TConfig dependencyServiceConfig);
    }
}
