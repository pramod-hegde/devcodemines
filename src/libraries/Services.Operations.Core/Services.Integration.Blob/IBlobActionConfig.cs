using Services.Integration.Core;
using Microsoft.Extensions.Caching.Memory;
using Services.Core.Logging;
using Services.Data.Blob;

namespace Services.Integration.Blob
{
    public delegate IExternalIntegrationMetadata BlobMetadataHandler(string dependentServiceAction);
    public interface IBlobActionConfig
    {
        ILogger Logger { get; set; }
        IMemoryCache Cache { get; set; }
        IBlobHandler BlobHandler { get; set; }
        BlobMetadataHandler OnActionInvoke { get; set; }
        bool ValidateBlobContentAfterWrite { get; set; }
        bool IncludeDelayInValidatingBlobContentAfterWrite { get; set; }
        int BlobContentValidationDelayInSeconds { get; set; }
    }
}
