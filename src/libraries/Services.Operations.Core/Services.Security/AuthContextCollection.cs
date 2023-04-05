using System;
using System.Collections;
using System.Collections.Generic;

namespace Services.Security
{
    public sealed class AuthContextCollection : IDictionary<string, object>
    {
        private Dictionary<string, object> __internalItemCollection;

        public AuthContextCollection()
        {
            __internalItemCollection = new Dictionary<string, object>();
        }

        public AuthContextCollection(int capacity)
        {
            __internalItemCollection = new Dictionary<string, object>(capacity);
        }

        public object this[string key]
        {
            get
            {
                if (__internalItemCollection.ContainsKey(key))
                {
                    return __internalItemCollection[key];
                }
                else
                {
                    return default;
                }
            }
            set
            {
                if (!__internalItemCollection.ContainsKey(key))
                {
                    __internalItemCollection.Add(key, value);
                }
                else
                {
                    __internalItemCollection[key] = value;
                }
            }
        }

        public ICollection<string> Keys => __internalItemCollection.Keys;

        public ICollection<object> Values => __internalItemCollection.Values;

        public int Count => __internalItemCollection.Count;

        public bool IsReadOnly => false;

        public void Add(string key, object value)
        {
            __internalItemCollection.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            __internalItemCollection.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            __internalItemCollection.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return __internalItemCollection.ContainsKey(item.Key) && __internalItemCollection.ContainsValue(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return __internalItemCollection.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return __internalItemCollection.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return __internalItemCollection.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return __internalItemCollection.Remove(item.Key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return __internalItemCollection.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return __internalItemCollection.GetEnumerator();
        }
    }
}
