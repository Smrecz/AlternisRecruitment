using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Recruitment.API.Controllers;
using Recruitment.API.HttpClients;
using Recruitment.Contracts.API;
using Recruitment.Contracts.Encryption;
using Xunit;

namespace Recruitment.Tests.Controllers
{
    public class EncryptionControllerTests
    {
        [Fact]
        public async Task Hash_Should_Return_Calculated_Hash_Response()
        {
            //Arrange
            var credentials = new CredentialsDto();

            var expectedResponse = new Md5Response("someHash");

            var expectedResult = new JsonResult(expectedResponse);

            var httpClient = TestFactory.CreateSuccessHttpClient(expectedResponse);

            var encryptionClient = new EncryptionClient(httpClient);

            var encryptionController = new EncryptionController(encryptionClient);

            //Act
            var result = await encryptionController.Hash(credentials);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Hash_Should_Throw_On_Error()
        {
            //Arrange
            var credentials = new CredentialsDto();

            var httpClient = TestFactory.CreateErrorHttpClient();

            var encryptionClient = new EncryptionClient(httpClient);

            var encryptionController = new EncryptionController(encryptionClient);

            //Act
            Func<Task> action = () => encryptionController.Hash(credentials);

            //Assert
            await action.Should().ThrowAsync<HttpRequestException>();
        }
    }
}
