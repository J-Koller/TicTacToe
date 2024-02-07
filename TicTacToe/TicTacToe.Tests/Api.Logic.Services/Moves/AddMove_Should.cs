using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Shared.Dto;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Moves
{
    public class AddMove_Should
    {
        [Fact]
        public async Task ThrowIf_Argument_MoveDtos_IsNull()
        {
            // Arrange

            var sut = new MoveService();

            // Act

            var testingCode = () => sut.AddMoveAsync(null, 1);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>("moveDtos", testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_GameId_IsLessThan_One()
        {
            // Arrange

            List<MoveDto> moveDtos = new List<MoveDto>();

            var sut = new MoveService();

            // Act

            var testingCode = () => sut.AddMoveAsync(moveDtos, 0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_MoveDtos_HasAny_Nulls()
        {
            // Arrange

            List<MoveDto> moveDtos = new List<MoveDto>
            {
                null,
                new()
            };

            var sut = new MoveService();

            // Act

            var testingCode = () => sut.AddMoveAsync(moveDtos, 1);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }
    }
}
