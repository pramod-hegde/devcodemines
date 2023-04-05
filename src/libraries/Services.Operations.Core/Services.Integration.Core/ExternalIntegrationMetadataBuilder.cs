using System.Collections.Generic;
using System.Linq;

namespace Services.Integration.Core
{
    public sealed class ExternalIntegrationMetadataBuilder
    {
        ExternalIntegrationMetadataSet __set;

        public ExternalIntegrationMetadataBuilder()
        {
            if (__set == null)
            {
                __set = new ExternalIntegrationMetadataSet();
            }
        }

        public ExternalIntegrationMetadataBuilder(int capacity)
        {
            if (__set == null)
            {
                __set = new ExternalIntegrationMetadataSet(capacity);
            }
        }

        public ExternalIntegrationMetadataBuilder(KeyValuePair<string, IExternalIntegrationMetadata> serviceMetadata) : this()
        {
            if (__set != null)
            {
                __set.Add(serviceMetadata);
            }
        }

        public ExternalIntegrationMetadataBuilder(IEnumerable<IExternalIntegrationMetadata> serviceMetadataCollection) : this(serviceMetadataCollection.ToArray())
        {
        }

        public ExternalIntegrationMetadataBuilder(params IExternalIntegrationMetadata[] serviceMetadataCollection) : this()
        {
            if (__set != null)
            {
                foreach (var serviceMetadata in serviceMetadataCollection)
                {
                    __set.Add(serviceMetadata.ServiceType, serviceMetadata);
                }
            }
        }

        public ExternalIntegrationMetadataBuilder(string serviceType, IExternalIntegrationMetadata metadata) : this()
        {
            if (__set != null)
            {
                __set.Add(serviceType, metadata);
            }
        }

        public ExternalIntegrationMetadataBuilder Add(KeyValuePair<string, IExternalIntegrationMetadata> serviceMetadata)
        {
            if (!__set.ContainsKey(serviceMetadata.Key))
            {
                __set.Add(serviceMetadata);
            }

            return this;
        }

        public ExternalIntegrationMetadataBuilder Add(IExternalIntegrationMetadata serviceNode)
        {
            if (!__set.ContainsKey(serviceNode.ServiceType))
            {
                __set.Add(serviceNode.ServiceType, serviceNode);
            }

            return this;
        }

        public ExternalIntegrationMetadataBuilder Add(string serviceType, IExternalIntegrationMetadata metadata)
        {
            if (!__set.ContainsKey(serviceType))
            {
                __set.Add(serviceType, metadata);
            }

            return this;
        }

        public ExternalIntegrationMetadataSet ToMetadataSet()
        {
            return __set;
        }
    }
}
