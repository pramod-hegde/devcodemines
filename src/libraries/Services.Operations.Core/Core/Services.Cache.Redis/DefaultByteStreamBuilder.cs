using Services.Cache.Contracts;
using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace Services.Cache.Redis
{
    class DefaultByteStreamBuilder : ICacheItemBuilder
    {
        dynamic ICacheItemBuilder.BuildCacheItem (object value)
        {
            if (value is byte[])
            {
                return value as byte[];
            }

            byte[] inb = GetSerializedValue(value);
            return GetCompressedValue(inb);
        }

        private byte[] GetCompressedValue (byte[] inbytes)
        {
            byte[] outbytes;
            using (MemoryStream ostream = new MemoryStream())
            {
                using (DeflateStream df = new DeflateStream(ostream, CompressionMode.Compress, true))
                {
                    df.Write(inbytes, 0, inbytes.Length);
                }
                outbytes = ostream.ToArray();
            }

            return outbytes;
        }

        private byte[] GetSerializedValue (object value)
        {
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException("Object is not serializable");
            }

            using (MemoryStream stream = new MemoryStream())
            {
                DoXmlSerialize(value, stream);
                return stream.ToArray();
            }
        }

        private void DoXmlSerialize (object value, MemoryStream stream)
        {
            using (XmlWriter wtr = XmlWriter.Create(stream))
            {
                XmlSerializer serializer = new XmlSerializer(value.GetType());
                serializer.Serialize(wtr, value);
                stream.Flush();
            }
        }
    }
}
