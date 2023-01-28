using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IL2CareerModel.Services;
internal class Md5ChecksumService : IFileChecksumService
{
    public string GetChecksum(string path)
    {
        string returnString = string.Empty;
        if (!File.Exists(path))
        {
            return returnString;
        }
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(path))
            {
                returnString = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
            }
        }
        return returnString;
    }
}
