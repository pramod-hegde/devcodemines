using System;
using System.ComponentModel;

namespace Services.Core.Contracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class StoreTypeAttribute : Attribute
    {
        public CacheStore Store { get; } = CacheStore.None;
        public StoreTypeAttribute (CacheStore store)
        {
            Store = store;
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
    }
}
