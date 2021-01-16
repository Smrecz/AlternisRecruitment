using System.Diagnostics.CodeAnalysis;

namespace Recruitment.Contracts.Encryption
{
    [ExcludeFromCodeCoverage]
    public class Md5Response
    {
        public Md5Response(string hashValue) => 
            HashValue = hashValue;

        public string HashValue { get; }
    }
}
