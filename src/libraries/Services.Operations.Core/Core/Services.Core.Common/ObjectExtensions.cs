using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Services.Core.Common
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static T Clone<T> (this T message)
        {
            if (message == null)
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, message);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Converts bytes into a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T As<T> (this byte[] bytes)
        {
            if (bytes == null || !bytes.Any())
            {
                return default(T);
            }

            int offset = 0;
            var type = typeof(T);

            if (type == typeof(sbyte)) return (T)(object)((sbyte)bytes[offset]);
            else if (type == typeof(byte)) return (T)(object)bytes[offset];
            else if (type == typeof(short)) return (T)(object)BitConverter.ToInt16(bytes, offset);
            else if (type == typeof(ushort)) return (T)(object)BitConverter.ToUInt16(bytes, offset);
            else if (type == typeof(int)) return (T)(object)BitConverter.ToInt32(bytes, offset);
            else if (type == typeof(uint)) return (T)(object)BitConverter.ToUInt32(bytes, offset);
            else if (type == typeof(long)) return (T)(object)BitConverter.ToInt64(bytes, offset);
            else if (type == typeof(ulong)) return (T)(object)BitConverter.ToUInt64(bytes, offset);
            else if (type == typeof(string)) return (T)(object)Encoding.Default.GetString(bytes).Trim('"');
            else if (type == typeof(double)) return (T)(object)BitConverter.ToDouble(bytes, offset);
            else if (type == typeof(float)) return (T)(object)BitConverter.ToSingle(bytes, offset);
            else if (type == typeof(bool)) return (T)(object)BitConverter.ToBoolean(bytes, offset);

            else throw new NotSupportedException($"Type conversion is not supported for this {type}");
        }
    }
}
