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
    public class JoinGameAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly Mock<IMoveService> _moveServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly Player _playerOne;

        private readonly Player _playerTwo;

        private readonly PlayerDto _playerOneDto;

        private readonly Game _game;

        public JoinGameAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _moveServiceMock = new Mock<IMoveService>();
            _mapperMock = new Mock<IMapper>();

            _playerOne = new Player
            {
                Id = 1
            };

            _playerTwo = new Player
            {
                Id = 2
            };

            _playerOneDto = new PlayerDto
            {
                Id = 1
            };

            _game = new Game
            {
                PlayerOneGameConnectionId = "a connection id",
                PlayerOneId = 1
            };
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_ConnectionId_IsNull()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 1,
                GameId = 1,
                GameHubConnectionId = null
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(joinGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_ConnectionId_IsEmpty()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 1,
                GameId = 1,
                GameHubConnectionId = ""
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(joinGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }


        [Fact]
        public async Task ThrowIf_Argument_PlayerId_IsLessThan_One()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 0,
                GameId = 1,
                GameHubConnectionId = "connectionId"
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(joinGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_GameId_IsLessThan_One()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 1,
                GameId = 0,
                GameHubConnectionId = "connectionId"
            };

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(joinGameDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_PlayerIsAlreadyInGame()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 1,
                GameId = 1,
                GameHubConnectionId = "connectionId"
            };

            var game = new Game
            {
            };

            game.Players.Add(_playerOne);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(game);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.JoinGameAsync(joinGameDto);

            // Assert

            await Assert.ThrowsAsync<InvalidOperationException>(testingCode);
        }

        [Fact]
        public async Task Add_PlayertoGame()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 2,
                GameId = 1,
                GameHubConnectionId = "connectionId"
            };

           
            _game.Players.Add(_playerOne);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_playerOne);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(2)).ReturnsAsync(_playerTwo);
            _mapperMock.Setup(mm => mm.Map<PlayerDto>(_playerOne)).Returns(_playerOneDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                _playerRepositoryMock.Object,
                null,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var playerOneDto = await sut.JoinGameAsync(joinGameDto);

            // Assert

            Assert.True(_game.Players.Count == 2);
        }

        [Fact]
        public async Task Call_SaveChanges()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 2,
                GameId = 1,
                GameHubConnectionId = "connectionId"
            };

            _game.Players.Add(_playerOne);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_playerOne);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(2)).ReturnsAsync(_playerTwo);
            _mapperMock.Setup(mm => mm.Map<PlayerDto>(_playerOne)).Returns(_playerOneDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                _playerRepositoryMock.Object,
                null,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var playerOneDto = await sut.JoinGameAsync(joinGameDto);

            // Assert

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var joinGameDto = new JoinGameDto
            {
                PlayerId = 2,
                GameId = 1,
                GameHubConnectionId = "connectionId"
            };

            _game.Players.Add(_playerOne);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_playerOne);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(2)).ReturnsAsync(_playerTwo);
            _mapperMock.Setup(mm => mm.Map<PlayerDto>(_playerOne)).Returns(_playerOneDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                _playerRepositoryMock.Object,
                null,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var playerOneDto = await sut.JoinGameAsync(joinGameDto);

            // Assert

            Assert.NotNull(playerOneDto);
        }
    }
}
