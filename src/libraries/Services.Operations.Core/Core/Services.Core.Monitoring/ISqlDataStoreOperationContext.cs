using System;

namespace Services.Core.Monitoring.Primitives
{
    public interface ISqlDataStoreOperationContext
    {
        string Id { get; set; }
        string SqlConnection { get; set; }
        TimeSpan? OperationTimeout { get; set; }
        DataOperationTypes Operation { get; set; }
    }
}
