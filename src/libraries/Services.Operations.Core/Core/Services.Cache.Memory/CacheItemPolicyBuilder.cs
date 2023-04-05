using Services.Core.Contracts;
using System.Runtime.Caching;

namespace Services.Cache.Memory
{
    class CacheItemPolicyBuilder
    {
        ICacheItemPolicy _policy;
        public CacheItemPolicyBuilder (ICacheItemPolicy policy)
        {
            _policy = policy;
        }

        internal CacheItemPolicy Build ()
        {
            var p = new CacheItemPolicy();

            if (_policy.IsAbsoluteExpiration)
            {
                p.AbsoluteExpiration = _policy.AbsoluteExpiration;
            }
            else
            {
                p.SlidingExpiration = _policy.SlidingExpiration;
            }

            return p;
        }
    }
}
