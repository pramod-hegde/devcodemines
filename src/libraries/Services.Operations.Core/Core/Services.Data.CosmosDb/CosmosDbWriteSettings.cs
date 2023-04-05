namespace Services.Data.CosmosDb
{
    public class CosmosDbWriteSettings
    {
        public CosmosDbAction Action;
        public object PartitionKeyValue;
    }
}
