using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recruitment.Functions.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<string> ReadToStringAsync(this HttpRequest request)
        {
            request.EnableBuffering();

            using var reader = new StreamReader(request.Body);

            var result = await reader.ReadToEndAsync();

            request.Body.Seek(0, SeekOrigin.Begin);

            return result;
        }
    }
}
