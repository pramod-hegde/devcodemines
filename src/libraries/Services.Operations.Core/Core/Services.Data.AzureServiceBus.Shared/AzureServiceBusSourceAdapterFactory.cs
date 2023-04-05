using Services.Core.Common;
using Services.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.AzureServiceBus
{
    public sealed class AzureServiceBusSourceAdapterFactory<TCache> : DataAdapterFactoryBase, IDataAccessAdapterFactory<IAzureServiceBusAccessAdapterConfiguration<TCache>>
    {
        public string Description
        {
            get { return "This adapter implements data access classes for AzureServiceBus"; }
        }

        public Task<IDataAccessAdapter> CreateAsync(IAzureServiceBusAccessAdapterConfiguration<TCache> configuration, IDataAccessContext context, CancellationToken cancellation)
        {
            return Task.Factory.StartNew(() => Create(configuration), cancellation);
        }

        private static IDataAccessAdapter Create(IAzureServiceBusAccessAdapterConfiguration<TCache> configuration)
        {
            Ensure.NotNull("configuration", configuration);

            if (configuration.Connection == null ||
                (!configuration.Connection.UseManagedIdentity && string.IsNullOrWhiteSpace(configuration.Connection.ConnectionString)) ||
                (configuration.Connection.UseManagedIdentity && string.IsNullOrWhiteSpace(configuration.Connection.Namespace)))
                throw LocalErrors.ConnectionStringMissing();

            return new AzureServiceBusAccessAdapter<TCache>(CreateInstanceConfiguration(configuration));
        }

        private static IAzureServiceBusAccessAdapterInstanceConfiguration<TCache> CreateInstanceConfiguration(IAzureServiceBusAccessAdapterConfiguration<TCache> configuration)
        {
            return new AzureServiceBusSourceAdapterInstanceConfiguration<TCache>
            {
                Connection = configuration.Connection,
                LocationMode = configuration.LocationMode,
                EnableSharding = configuration.EnableSharding,
                MaximumPartitionIndex = configuration.MaximumPartitionIndex,
                MinimumPartitionIndex = configuration.MinimumPartitionIndex,
                Id = configuration.Id,
                Queue = configuration.Queue,
                Subscription = configuration.Subscription,
                TargetType = configuration.TargetType,
                Topic = configuration.Topic,
                MessageHandlers = configuration.MessageHandlers,
                CacheManager = configuration.CacheManager,
                Retries = configuration.Retries,
                RetryInterval = configuration.RetryInterval
            };
        }
    }
}
