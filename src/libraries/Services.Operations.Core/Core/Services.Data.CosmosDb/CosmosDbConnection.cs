using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Data.CosmosDb
{
    public class CosmosDbConnection
    {
        public string AccountEndPoint { get; set; }
        public string AccountKey { get; set; }
        public string Database { get; set; }
        public bool UseManagedIdentity { get; set; }
        public string TenantId { get; set; }
        public string ClientConnectionMode { get; set; } = "DirectTcp";
    }
}
