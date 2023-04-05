namespace Services.Cache.Redis
{
    enum RedisDataDistributionType
    {
        Default = 0,
        RoundRobbin = 1,
        EqualWeightage = 2
    }
}
