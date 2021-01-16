using System.Diagnostics.CodeAnalysis;

namespace Recruitment.Contracts.API
{
    [ExcludeFromCodeCoverage]
    public class CredentialsDto
    {
        public string Password { get; set; }
        public string Login { get; set; }
    }
}
