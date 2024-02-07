using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Games
{
    public class StartNewGameAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly Mock<IMoveService> _moveServiceMock;

        private readonly NewGameDto _newGameDto;
        private readonly Player _playerOne;
        private readonly Game _newGame;

        public StartNewGameAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _moveServiceMock = new Mock<IMoveService>();

            _newGameDto = new NewGameDto
            {
                PlayerId = 1,
                GameHubConnectionId = "connectionId"
            };

            _playerOne = new Player
            {
                Id = 1
            };

            _newGame = new Game
            {
                Id = 1
            };
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.StartNewGameAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_ConnectionId_IsNull()
        {
            // Arrange

            var newGameDto = new NewGameDto()
            {
                PlayerId = 1,
                GameHubConnectionId = null
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.StartNewGameAsync(newGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_ConnectionId_IsEmpty()
        {
            // Arrange

            var newGameDto = new NewGameDto()
            {
                PlayerId = 1,
                GameHubConnectionId = ""
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.StartNewGameAsync(newGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_PlayerId_IsLessThanOne()
        {
            // Arrange

            var newGameDto = new NewGameDto()
            {
                PlayerId = 0,
                GameHubConnectionId = "connectionString"
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.StartNewGameAsync(newGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task SaveNewGame()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_playerOne);

            _gameRepositoryMock.Setup(grm => grm.AddAsync(It.IsAny<Game>())).ReturnsAsync(1);

            var sut = new GameService(_gameRepositoryMock.Object, _playerRepositoryMock.Object, null, _moveServiceMock.Object, null);

            // Act

            int newGameId = await sut.StartNewGameAsync(_newGameDto);

            // Assert

            _gameRepositoryMock.Verify(grm => grm.AddAsync(It.IsAny<Game>()));
            Assert.True(newGameId > 0);
        }
    }
}
