using Services.Core.Common;
using Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.CosmosDb
{
    public class CosmosDbSourceAdapterFactory<TCache> : DataAdapterFactoryBase, IDataAccessAdapterFactory<ICosmosDbAccessAdapterInstanceConfiguration<TCache>>
    {
        public string Description
        {
            get { return "This adapter implements data access classes for CosmosDb"; }
        }

        public Task<IDataAccessAdapter> CreateAsync(ICosmosDbAccessAdapterInstanceConfiguration<TCache> configuration, IDataAccessContext context, CancellationToken cancellation)
        {
            return Task.Factory.StartNew(() => Create(configuration), cancellation);
        }

        private static IDataAccessAdapter Create(ICosmosDbAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            Ensure.NotNull("configuration", configuration);

            return new CosmosDbAccessAdapter<TCache>(CreateInstanceConfiguration(configuration));
        }

        private static ICosmosDbAccessAdapterInstanceConfiguration<TCache> CreateInstanceConfiguration(ICosmosDbAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            return new CosmosDbSourceAdapterInstanceConfiguration<TCache>
            {
                Connection = configuration.Connection,
                Collection = configuration.Collection,
                CacheManager = configuration.CacheManager,
                Retries = configuration.Retries,
                RetryInterval = configuration.RetryInterval,
                UseReadOnlyConnection = configuration.UseReadOnlyConnection,
                PreferredReadLocations = configuration.PreferredReadLocations,
                Handlers = configuration.Handlers
            };
        }
    }
}
