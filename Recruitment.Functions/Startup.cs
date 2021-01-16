using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.Functions.Services;

[assembly: FunctionsStartup(typeof(Recruitment.Functions.Startup))]
namespace Recruitment.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IEncryptionService, EncryptionService>();
        }
    }
}
