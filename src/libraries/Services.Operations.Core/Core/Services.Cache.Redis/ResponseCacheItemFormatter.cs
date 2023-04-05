using System.Linq;
using System.IO;
using System.IO.Compression;

namespace Services.Cache.Redis
{
    public sealed partial class RedisCacheManager
    {
        class ResponseCacheItemFormatter
        {
            internal byte[] Format (byte[] v)
            {
                if (v == null || !v.Any())
                {
                    return null;
                }

                byte[] outb;
                using (MemoryStream istream = new MemoryStream(v))
                {
                    using (MemoryStream ostream = new MemoryStream())
                    {
                        using (DeflateStream sr =
                            new DeflateStream(istream, CompressionMode.Decompress))
                        {
                            sr.CopyTo(ostream);
                        }
                        outb = ostream.ToArray();
                    }
                }
                return outb;
            }
        }
    }
}
