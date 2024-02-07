using System;
using TicTacToe.Api.Logic.Services.Strings;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Strings
{
    public class IsAlphaNumeric_Should
    {
        [Fact]
        public void ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new StringService();

            // Act

            Func<object> testingCode = () => sut.IsAlphaNumeric(null);

            // Assert

            Assert.Throws<ArgumentNullException>(testingCode);
        }
    }
}
