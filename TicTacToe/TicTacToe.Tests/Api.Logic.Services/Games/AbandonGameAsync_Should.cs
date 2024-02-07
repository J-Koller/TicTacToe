using Moq;
using System.Threading.Tasks;
using System;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Repositories.Games;
using Xunit;
using TicTacToe.Data.Entities;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TicTacToe.Tests.Api.Logic.Services.Games
{
    public class AbandonGameAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IMoveService> _moveServiceMock;
        private readonly GameResultDto _gameResultDto;
        private readonly Game _game;
        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public AbandonGameAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _moveServiceMock = new Mock<IMoveService>();

            _gameResultDto = new GameResultDto
            {
                GameId = 1,
                WinnerId = 1,
                IsTie = false
            };

            _playerOne = new Player { Id = 1 };
            _playerTwo = new Player { Id = 2 };

            _game = new Game
            {
                Id = 1,
                Players = new List<Player> { _playerOne}
            };
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new GameService(
                null,
                null,
                null,
                _moveServiceMock.Object,
                null);

            // Act

            var testingCode = () => sut.AbandonGameAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_GameId_IsLessThan_One()
        {
            // Arrange

            var sut = new GameService(
                null,
                null,
                null,
                _moveServiceMock.Object,
                null);

            _gameResultDto.GameId = 0;

            // Act

            var testingCode = () => sut.AbandonGameAsync(_gameResultDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Game_HasJustOnePlayer_And_Argument_WinnerId_IsLessThan_One()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            _gameResultDto.WinnerId = 0;
            _game.Players.Add(_playerTwo);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            var testingCode = () => sut.AbandonGameAsync(_gameResultDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task Assign_FalseTo_Game_IsLookingForPlayers_When_GameHasJustOnePlayer_And_FinishGame()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.AbandonGameAsync(_gameResultDto);

            // Assert

            Assert.False(_game.IsLookingForPlayers);
            Assert.True(_game.FinishDate.HasValue);

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }

        [Fact]
        public async Task Assign_WinnerAndLoser_When_GameHasTwoPlayers()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            _game.Players.Add(_playerTwo);

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.AbandonGameAsync(_gameResultDto);

            // Assert

            Assert.True(_game.WinnerId.HasValue && _game.LoserId.HasValue);
            Assert.True(_game.FinishDate.HasValue);

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }
    }
}
