namespace Services.Data.Blob
{
    public class BlobWriteSettings
    {
        public BlobWriteActions Action;
        public string Container;
        public string BlobName;
        public bool CompressWhileStore;

        public BlobWriteSettings(bool compressWhileStore = true)
        {
            CompressWhileStore = compressWhileStore;
        }
    }
}
