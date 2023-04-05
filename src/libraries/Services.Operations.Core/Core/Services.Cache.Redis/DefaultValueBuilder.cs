using Services.Cache.Contracts;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Services.Cache.Redis
{
    sealed class DefaultValueBuilder : ICacheItemBuilder
    {
        dynamic ICacheItemBuilder.BuildCacheItem (object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                return value as string;
            }

            if (value is byte[])
            {
                return Encoding.Default.GetString(value as byte[]);
            }

            if (value.GetType().IsValueType)
            {
                return Convert.ToString(value);
            }

            return GetRedisValueForComplexType(value);
        }

        private dynamic GetRedisValueForComplexType (object value)
        {
            byte[] inBytes = Serialize(value);
            using (var ms = new MemoryStream())
            {
                using (DeflateStream df = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    df.Write(inBytes, 0, inBytes.Length);
                }
                return ms.ToArray();
            }
        }

        private byte[] Serialize (object value)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(value.GetType(), GetKnownTypes());
                ser.WriteObject(ms, value);
                return ms.ToArray();
            }
        }

        private Type[] GetKnownTypes ()
        {
            return new[] { typeof(DBNull) };
        }
    }
}
