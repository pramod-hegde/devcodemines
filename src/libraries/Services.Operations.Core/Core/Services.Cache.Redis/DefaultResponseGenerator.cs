using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace Services.Cache.Redis
{
    sealed class DefaultResponseGenerator
    {
        internal object Generate (byte[] response)
        {
            if (response == null || !response.Any())
            {
                return null;
            }

            byte[] outBytes;
            using (MemoryStream rStream = new MemoryStream(response))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (DeflateStream sr = new DeflateStream(rStream, CompressionMode.Decompress))
                    {
                        sr.CopyTo(outStream);
                    }
                    outBytes = outStream.ToArray();
                }
            }

            return outBytes;
        }

        internal T Generate<T> (byte[] response)
        {
            return DeSerialize<T>((byte[])Generate(response));
        }

        private T DeSerialize<T> (byte[] outBytes)
        {
            using (MemoryStream ms = new MemoryStream(outBytes))
            {
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
                return (T)s.ReadObject(ms);
            }
        }
    }
}
