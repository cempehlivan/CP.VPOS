using System;
using System.Security.Cryptography;
using System.Text;

namespace CP.VPOS.Infrastructures.Iyzico
{
    internal sealed class HashGenerator
    {
        private HashGenerator()
        {
        }

        public static String GenerateHash(String apiKey, String secretKey, String randomString, BaseRequest request)
        {
#pragma warning disable SYSLIB0021
#if NETSTANDARD
            SHA1 algorithm = SHA1.Create();
#else
            HashAlgorithm algorithm = new SHA1Managed();
#endif
#pragma warning restore SYSLIB0021
            string hashStr = apiKey + randomString + secretKey + request.ToPKIRequestString();
            byte[] computeHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashStr));
            return Convert.ToBase64String(computeHash);
        }
    }
}
