using Services.Cache.Contracts;

namespace Services.Cache.Redis
{
    abstract class DistributionTypeBase
    {
        protected IRedisCacheProviderConfiguration _config;

        internal DistributionTypeBase (IRedisCacheProviderConfiguration config)
        {
            _config = config;
        }

        internal abstract int NextIndex ();
    }
}
