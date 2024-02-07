using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Games
{
    public class GetAvailableGamesAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IMoveService> _moveServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAvailableGamesAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _moveServiceMock = new Mock<IMoveService>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, _mapperMock.Object);

            // Act

            var availableGameDtos = await sut.GetAvailableGamesAsync();

            // Assert

            Assert.NotNull(availableGameDtos);
        }

        [Fact]
        public async Task Return_AllAvailableGameDtos()
        {
            // Arrange

            var availableGames = new List<Game>
            {
                new Game(),
                new Game()
            };

            var expectedAvailableGameDtos = new List<AvailableGameDto>
            {
                new AvailableGameDto(),
                new AvailableGameDto()
            };

            _gameRepositoryMock.Setup(grm => grm.GetGamesLookingForPlayersAsync()).ReturnsAsync(availableGames);

            _mapperMock.Setup(mm => mm.Map<IEnumerable<AvailableGameDto>>(availableGames)).Returns(expectedAvailableGameDtos);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, _mapperMock.Object);

            // Act

            var actualAvailableGameDtos = await sut.GetAvailableGamesAsync();

            // Assert

            Assert.Equal(availableGames.Count, actualAvailableGameDtos.Count());
        }
    }
}
