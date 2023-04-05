using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Services.Data.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Compress(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            try
            {
                var bytes = Encoding.Unicode.GetBytes(value);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        msi.CopyTo(gs);
                    }
                    return Convert.ToBase64String(mso.ToArray());
                }
            }
            catch
            {
                return value;
            }
        }

        public static Stream CompressStream(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            try
            {
                Stream returnstream = default;
                var bytes = Encoding.Unicode.GetBytes(value);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        msi.CopyTo(gs);
                    }
                    returnstream = mso;
                }

                return returnstream;
            }
            catch(Exception ex)
            {
                return default;
            }
        }

        public static string Decompress(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            try
            {
                var bytes = Convert.FromBase64String(value);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        gs.CopyTo(mso);
                    }
                    return Encoding.Unicode.GetString(mso.ToArray());
                }
            }
            catch
            {
                return value;
            }
        }

        public static string DecompressStream(this Stream value)
        {
            try
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(value, CompressionMode.Decompress))
                    {
                        gs.CopyTo(mso);
                    }
                    return Encoding.Unicode.GetString(mso.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
