using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;

namespace Recruitment.Tests
{
    public class TestFactory
    {
        public static HttpRequest CreateHttpRequestFromObject<T>(T input)
        {
            var json = JsonSerializer.Serialize(input);

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Body = memoryStream;
            request.ContentType = "application/json";

            return request;
        }

        public static HttpClient CreateSuccessHttpClient<T>(T content) => 
            CreateHttpClient(CreateSuccessHttpResponse(content));

        public static HttpClient CreateErrorHttpClient() =>
            CreateHttpClient(CreateErrorHttpResponse());

        private static HttpClient CreateHttpClient(HttpResponseMessage responseMessage)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://fake")
            };

            return httpClient;
        }

        private static HttpResponseMessage CreateSuccessHttpResponse<T>(T content) =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = 
                    new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
            };

        private static HttpResponseMessage CreateErrorHttpResponse() =>
            new HttpResponseMessage(HttpStatusCode.InternalServerError);
    }
}
