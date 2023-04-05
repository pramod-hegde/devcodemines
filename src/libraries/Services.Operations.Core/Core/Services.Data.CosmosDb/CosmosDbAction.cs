namespace Services.Data.CosmosDb
{
    public enum CosmosDbAction
    {
        /// <summary>
        /// Insert document to CosmosDB
        /// </summary>
        Insert,

        /// <summary>
        /// Delete document from CosmosDB
        /// Delete works only with id. Makes sure to pass the documentId in the request instead of the actual document
        /// </summary>
        Delete,

        /// <summary>
        /// Upsert document to CosmosDB
        /// </summary>
        Upsert
    }
}
