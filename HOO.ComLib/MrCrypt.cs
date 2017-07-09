using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HOO.ComLib
{
    public class MrCrypt
    {
        //private System.Security.Cryptography.ECDiffieHellmanCng dhCng = new ECDiffieHellmanCng(256);
        public MrCrypt()
        {
            //dhCng.HashAlgorithm = CngAlgorithm.ECDiffieHellmanP256;
            //dhCng.
        }

        public static string GetMD5Hash(string source)
        {
            byte[] encodedSource = new UTF8Encoding().GetBytes(source);

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedSource);

            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }
    }
}
