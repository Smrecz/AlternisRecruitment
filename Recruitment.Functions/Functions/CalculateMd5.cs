using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Recruitment.Contracts.Encryption;
using Recruitment.Functions.Extensions;
using Recruitment.Functions.Services;

namespace Recruitment.Functions.Functions
{
    public class CalculateMd5
    {
        private readonly IEncryptionService _encryptionService;

        public CalculateMd5(IEncryptionService encryptionService) => 
            _encryptionService = encryptionService;

        [FunctionName(nameof(CalculateMd5))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function: '{nameof(CalculateMd5)}' processed a request.");

            var requestBody = await req.ReadToStringAsync();

            var hash = _encryptionService.CalculateMd5(requestBody);

            return new JsonResult(new Md5Response(hash));
        }
    }
}
