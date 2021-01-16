using System.Net.Http;
using System.Threading.Tasks;
using Recruitment.Contracts.API;
using Recruitment.Contracts.Encryption;

namespace Recruitment.API.HttpClients
{
    public class EncryptionClient
    {
        private const string CalculateMd5Address = "/api/CalculateMd5";

        private readonly HttpClient _httpClient;

        public EncryptionClient(HttpClient client) => 
            _httpClient = client;

        public async Task<Md5Response> GetMd5HashFromCredentials(CredentialsDto credentials)
        {
            var response = await _httpClient.PostAsJsonAsync(CalculateMd5Address, credentials);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Md5Response>();
        }
    }
}
