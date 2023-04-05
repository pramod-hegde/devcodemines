namespace Services.Data.Blob
{
    public class BlobQueryConfiguration
    {
        public string Container { get; set; }
        //public bool ReadAllBlobs { get; set; } // at present this is not supported. need to find an optimal way to read all the blobs of a container
        public string BlobName { get; set; }
        //public BlobNameCompareOptions NameCompareOptions { get; set; }  // at present this is not supported. we can read only one blob. need to find an optimal way to read all the blobs which starts with the BlobName
        public string BlobUri { get; set; }
        public BlobQueryTypes QueryType { get; set; }
        public ContentFormattingModes FormattingMode { get; set; }
    }
}
