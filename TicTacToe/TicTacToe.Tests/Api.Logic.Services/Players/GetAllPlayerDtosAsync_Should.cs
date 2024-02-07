using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class GetAllPlayerDtosAsync_Should
    {
        [Fact]
        public async Task Return_AllPlayerDtos()
        {
            // Arrange
            var expectedPlayers = new List<Player>
            {
                new Player()
            };

            var expectedPlayersDto = new List<PlayerDto>
            {
                new PlayerDto()
            };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(expectedPlayers);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<IEnumerable<PlayerDto>>(expectedPlayers)).Returns(expectedPlayersDto);

            var sut = new PlayerService(playerRepositoryMock.Object, null, null, mapperMock.Object);

            // Act

            var actualPlayerDtos = await sut.GetAllPlayerDtosAsync();

            // Assert

            Assert.Equal(expectedPlayers.Count, actualPlayerDtos.Count());
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var expectedPlayers = new List<Player>
            {
                new Player()
            };
            
            var expectedPlayersDto = new List<PlayerDto>
            {
                new PlayerDto()
            };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(expectedPlayers);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<IEnumerable<PlayerDto>>(expectedPlayers)).Returns(expectedPlayersDto);

            var sut = new PlayerService(playerRepositoryMock.Object, null, null, mapperMock.Object);

            // Act

            var actualPlayerDtos = await sut.GetAllPlayerDtosAsync();

            // Assert

            Assert.NotNull(actualPlayerDtos);
            //Assert.NotEqual(null, actualPlayerDtos);
        }
    }
}
