using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Recruitment.API.HttpClients;
using Recruitment.Contracts.API;

namespace Recruitment.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class EncryptionController : ControllerBase
    {
        private readonly EncryptionClient _encryptionClient;

        public EncryptionController(EncryptionClient encryptionClient) => 
            _encryptionClient = encryptionClient;

        [HttpPost("hash")]
        public async Task<IActionResult> Hash([FromBody] CredentialsDto credentials)
        {
            var response = await _encryptionClient.GetMd5HashFromCredentials(credentials);

            return new JsonResult(response);
        }
    }
}
