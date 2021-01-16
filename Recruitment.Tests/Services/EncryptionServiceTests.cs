using System;
using System.Collections.Generic;
using FluentAssertions;
using Recruitment.Functions.Services;
using Xunit;

namespace Recruitment.Tests.Services
{
    public class EncryptionServiceTests : MockerBase<EncryptionService>
    {
        [Fact]
        public void CalculateMd5_Should_Throw_On_Null_Input()
        {
            //Act
            Action action = () => GetInstance().CalculateMd5(null);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateMd5_Should_Return_Calculated_Hash(string input, string hash)
        {
            //Act
            var result = GetInstance().CalculateMd5(input);

            //Assert
            result.Should().Be(hash);
        }

        public static IEnumerable<object[]> TestData = new []
            {
                new object[] {string.Empty,"d41d8cd98f00b204e9800998ecf8427e"},
                new object[] {"SomeString","8b184827defd23262660b21bc49393ed"},
                new object[] {"LoginPassword","a6813a7b7d03b2b10590538bfa45391f"},
                new object[] {"SomeString","8b184827defd23262660b21bc49393ed"}
            };
    }
}
