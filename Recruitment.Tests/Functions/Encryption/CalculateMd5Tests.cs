using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Recruitment.Contracts.Encryption;
using Recruitment.Functions.Functions;
using Recruitment.Functions.Services;
using Xunit;

namespace Recruitment.Tests.Functions.Encryption
{
    public class CalculateMd5Tests : MockerBase<CalculateMd5>
    {
        [Fact]
        public async Task Function_Run_Should_Return_Calculated_Hash_Response()
        {
            //Arrange
            const string requestString = "TestInputString";
            const string responseString = "TestOutputString";

            AutoMocker.GetMock<IEncryptionService>()
                .Setup(x => x.CalculateMd5(It.IsAny<string>()))
                .Returns(responseString);

            var logger = new Mock<ILogger>().Object;

            var request = TestFactory.CreateHttpRequestFromObject(requestString);

            var expectedResult = new JsonResult(new Md5Response(responseString));
            
            //Act
            var result = await GetInstance().Run(request, logger);

            //Assert

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Triggered_Function_Should_Log_Information()
        {
            //Arrange
            const string expectedMessageLog = "C# HTTP trigger function: 'CalculateMd5' processed a request.";

            var loggerMock = new Mock<ILogger>();
            
            var request = TestFactory.CreateHttpRequestFromObject(string.Empty);

            //Act
            await GetInstance().Run(request, loggerMock.Object);

            //Assert
            loggerMock
                .VerifyLog(logger => logger
                    .LogInformation(expectedMessageLog));
        }
    }
}
