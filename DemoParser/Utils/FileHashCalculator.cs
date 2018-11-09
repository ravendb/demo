using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DemoParser.Utils
{
    public class FileHashCalculator
    {
        public string Get(string filePath)
        {
            using (var inputStream = new FileStream(filePath, FileMode.Open))
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(inputStream);
                var hashText = string.Join("", hash.Select(b => b.ToString("x2")));
                return hashText;
            }
        }
    }
}
