using Services.Cache.Contracts;

namespace Services.Cache.Redis
{
    sealed class RedisDbIndexAnalyzer
    {
        IRedisCacheProviderConfiguration _configuration;
        public RedisDbIndexAnalyzer (IRedisCacheProviderConfiguration config)
        {
            _configuration = config;
        }

        internal int GetNextIndex ()
        {
            switch (DistributionAlgorithm)
            {
                case RedisDataDistributionType.Default:
                default:
                    return new DefaultDataDistributionAlgorithm(_configuration).NextIndex();
            }
        }

        RedisDataDistributionType DistributionAlgorithm
        {
            get
            {
                return RedisDataDistributionType.Default; /// you can extend this by adding additional algo
            }
        }
    }
}
