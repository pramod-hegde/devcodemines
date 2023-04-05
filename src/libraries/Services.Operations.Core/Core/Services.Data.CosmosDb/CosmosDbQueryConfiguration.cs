namespace Services.Data.CosmosDb
{
    public class CosmosDbQueryConfiguration
    {
        public string Query { get; set; }
        public string PartionKey { get; set; }
        public bool UseIntegratedCache { get; set; }
        public int IntegratedCacheStalenessInMinutes { get; set; } = 300;
    }
}
