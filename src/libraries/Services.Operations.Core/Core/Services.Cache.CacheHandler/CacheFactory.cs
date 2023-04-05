using Services.Core.Contracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Services.Cache.CacheHandler
{
    public sealed class CacheFactory
    {
        static CacheFactory _defaultInstance;
        IList<ICacheManager> _internalCacheStores;
        static readonly object _mlock = new object();

        public static CacheFactory Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (_mlock)
                    {
                        _defaultInstance = new CacheFactory();
                    };
                }

                return _defaultInstance;
            }
        }

        private CacheFactory ()
        {
            Load();
        }

        void Load ()
        {
            var registeredStores = Core.Composition.Container.Default.GetAll<ICacheManager>();

            if (registeredStores == null || !registeredStores.Any())
            {
                throw LocalErrors.NoValidCacheStoreFound();
            }

            _internalCacheStores = registeredStores.ToList();
        }

        public ICacheManager GetHandle (CacheStore store)
        {
            if (store == CacheStore.None)
            {
                throw LocalErrors.NoValidCacheStoreFound();
            }

            return this[store];
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals (object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode ()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString ()
        {
            return base.ToString();
        }

        ICacheManager this[CacheStore storeType]
        {
            get
            {
                foreach (var m in _internalCacheStores)
                {
                    var a = (StoreTypeAttribute)m.GetType().GetCustomAttributes(typeof(StoreTypeAttribute), false).FirstOrDefault();

                    if (a == null)
                    {
                        continue;
                    }

                    if (a.Store == storeType)
                    {
                        return m;
                    }
                }

                throw LocalErrors.NoValidCacheStoreFound();
            }
        }
    }
}
