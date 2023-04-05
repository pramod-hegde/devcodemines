using System;
using System.Collections;
using System.Collections.Generic;

namespace Services.Integration.Core
{
    public sealed class ExternalIntegrationMetadataSet : IDictionary<string, IExternalIntegrationMetadata>
    {
        private Dictionary<string, IExternalIntegrationMetadata> __internalItemCollection;

        public ExternalIntegrationMetadataSet()
        {
            __internalItemCollection = new Dictionary<string, IExternalIntegrationMetadata>();
        }

        public ExternalIntegrationMetadataSet(int capacity)
        {
            __internalItemCollection = new Dictionary<string, IExternalIntegrationMetadata>(capacity);
        }

        public IExternalIntegrationMetadata this[string key]
        {
            get { return __internalItemCollection[key]; }
            set { __internalItemCollection.Add(key, value); }
        }

        public ICollection<string> Keys => __internalItemCollection.Keys;

        public ICollection<IExternalIntegrationMetadata> Values => __internalItemCollection.Values;

        public int Count => __internalItemCollection.Count;

        public bool IsReadOnly => false;

        public void Add(string key, IExternalIntegrationMetadata value)
        {
            __internalItemCollection.Add(key, value);
        }

        public void Add(KeyValuePair<string, IExternalIntegrationMetadata> item)
        {
            __internalItemCollection.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            __internalItemCollection.Clear();
        }

        public bool Contains(KeyValuePair<string, IExternalIntegrationMetadata> item)
        {
            return __internalItemCollection.ContainsKey(item.Key) && __internalItemCollection.ContainsValue(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return __internalItemCollection.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, IExternalIntegrationMetadata>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<KeyValuePair<string, IExternalIntegrationMetadata>> GetEnumerator()
        {
            return __internalItemCollection.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return __internalItemCollection.Remove(key);
        }

        public bool Remove(KeyValuePair<string, IExternalIntegrationMetadata> item)
        {
            return __internalItemCollection.Remove(item.Key);
        }

        public bool TryGetValue(string key, out IExternalIntegrationMetadata value)
        {
            return __internalItemCollection.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return __internalItemCollection.GetEnumerator();
        }
    }
}
