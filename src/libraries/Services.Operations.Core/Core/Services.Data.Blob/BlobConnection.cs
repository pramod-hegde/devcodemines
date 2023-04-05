namespace Services.Data.Blob
{
    public class BlobConnection 
    {
        public string AccountName { get; set; }
        public string SecureKey { get; set; }
        public bool UseManagedIdentity { get; set; }
    }
}
