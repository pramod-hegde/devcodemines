using Services.Cache.Contracts;

using System;

namespace Services.Cache.Redis
{
    sealed class DefaultDataDistributionAlgorithm : DistributionTypeBase
    {
        internal DefaultDataDistributionAlgorithm (IRedisCacheProviderConfiguration config) : base(config) { }

        internal override int NextIndex ()
        {
            Random random = new Random();
            return random.Next(0, _config.MaxDistributionIndex);
        }
    }
}
