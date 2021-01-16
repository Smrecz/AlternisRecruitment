using System;
using System.Text;
using System.Security.Cryptography;

namespace Recruitment.Functions.Services
{
    public class EncryptionService : IEncryptionService 
    {
        public string CalculateMd5(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", string.Empty)
                .ToLowerInvariant();

            return hash;
        }
    }
}
