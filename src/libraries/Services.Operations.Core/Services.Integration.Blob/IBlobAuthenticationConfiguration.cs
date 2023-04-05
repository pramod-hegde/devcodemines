using Microsoft.Extensions.Caching.Memory;
using Services.Data.Blob;

namespace Services.Integration.Blob
{
    public delegate IBlobAccessAdapterInstanceConfiguration<IMemoryCache> BlobConnectionAuthCallback();
    public interface IBlobAuthenticationConfiguration
    {
        object Configuration { get; set; }
        BlobConnectionAuthCallback AuthenticationCallback { get; }
    }
}
