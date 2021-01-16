using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.API.HttpClients;

namespace Recruitment.API.StartupExtensions
{
    public static class HttpExtensions
    {
        public static void AddCustomHttpClients(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var baseAddress = 
                new Uri(configuration.GetValue<string>("AzureFunctionsConfiguration:BaseUrl"));

            services.AddHttpClient<EncryptionClient>(c => c.BaseAddress = baseAddress);
        }
    }
}
