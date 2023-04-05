using Services.Core.Common;
using Services.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Blob
{
    public class BlobSourceAdapterFactory<TCache> : DataAdapterFactoryBase, IDataAccessAdapterFactory<IBlobAccessAdapterInstanceConfiguration<TCache>>
    {
        public string Description
        {
            get { return "This adapter implements data access classes for CosmosDb"; }
        }

        public Task<IDataAccessAdapter> CreateAsync(IBlobAccessAdapterInstanceConfiguration<TCache> configuration, IDataAccessContext context, CancellationToken cancellation)
        {
            return Task.Factory.StartNew(() => Create(configuration), cancellation);
        }

        private IDataAccessAdapter Create(IBlobAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            Ensure.NotNull("configuration", configuration);           

            return new BlobAccessAdapter<TCache>(CreateInstanceConfiguration(configuration));
        }

        private IBlobAccessAdapterInstanceConfiguration<TCache> CreateInstanceConfiguration(IBlobAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            return new BlobSourceAdapterInstanceConfiguration<TCache>
            {
                Connection = configuration.Connection,                
                CacheManager = configuration.CacheManager,
                Retries = configuration.Retries,
                RetryInterval = configuration.RetryInterval,
                BlobHandlers = configuration.BlobHandlers
            };
        }
    }
}
