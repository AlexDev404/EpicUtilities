using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnbr.Misc
{
    class misc
    {
        public static byte[] Compress(string text)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            byte[] result;
            using (MemoryStream memoryStream2 = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Compress))
                {
                    memoryStream.CopyTo(gzipStream);
                    gzipStream.Close();
                    result = memoryStream2.ToArray();
                }
            }
            return result;
        }

        public static string Decompress(byte[] compressed)
        {
            MemoryStream memoryStream = new MemoryStream();
            string result;
            using (MemoryStream memoryStream2 = new MemoryStream(compressed))
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(memoryStream);
                    gzipStream.Close();
                    memoryStream.Position = 0L;
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
            }
            return result;
        }
    }
}
