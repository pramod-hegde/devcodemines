using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Services.Data.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Stream Serialize<T>(this T value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                return stream;
            }
        }

        public static T Deserialize<T>(this Stream streamValue)
        {
            IFormatter formatter = new BinaryFormatter();
            streamValue.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(streamValue);
        }
    }
}
