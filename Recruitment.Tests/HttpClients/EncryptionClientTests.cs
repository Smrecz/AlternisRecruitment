using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Recruitment.API.HttpClients;
using Recruitment.Contracts.API;
using Recruitment.Contracts.Encryption;
using Xunit;

namespace Recruitment.Tests.HttpClients
{
    public class EncryptionClientTests
    {
        [Fact]
        public async Task GetMd5HashFromCredentials_Should_Return_Proper_Response()
        {
            // Arrange
            var expectedResponse = new Md5Response("someHash");

            var credentials = new CredentialsDto();

            var httpClient = TestFactory.CreateSuccessHttpClient(expectedResponse);

            var encryptionClient = new EncryptionClient(httpClient);

            //Act
            var result = await encryptionClient.GetMd5HashFromCredentials(credentials);

            //Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetMd5HashFromCredentials_Should_Throw_On_Not_Successful()
        {
            // Arrange
            var credentials = new CredentialsDto();

            var httpClient = TestFactory.CreateErrorHttpClient();

            var encryptionClient = new EncryptionClient(httpClient);

            //Act
            Func<Task> action = () => encryptionClient.GetMd5HashFromCredentials(credentials);

            //Assert
            await action.Should().ThrowAsync<HttpRequestException>();
        }
    }
}
