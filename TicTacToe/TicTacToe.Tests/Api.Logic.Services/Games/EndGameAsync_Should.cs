using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Games
{
    public class EndGameAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IMoveService> _moveServiceMock;

        private readonly Game _game;
        private readonly Player _winner;
        private readonly Player _loser;

        public EndGameAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _moveServiceMock = new Mock<IMoveService>();

            _winner = new Player
            {
                Id = 1
            };

            _loser = new Player
            {
                Id = 2
            };

            _game = new Game
            {
                Id = 1,
                Players = new List<Player> { _loser, _winner }
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

            var testingCode = () => sut.EndGameAsync(null);

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

            var gameResultDto = new GameResultDto
            {
                GameId = 0
            };

            // Act

            var testingCode = () => sut.EndGameAsync(gameResultDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsTie_And_WinnerId_IsGreaterThan_Zero()
        {
            // Arrange

            var sut = new GameService(
                null,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = true,
                WinnerId = 1
            };

            // Act

            var testingCode = () => sut.EndGameAsync(gameResultDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNotTie_And_WinnerId_IsLessThan_One()
        {
            // Arrange

            var sut = new GameService(
                null,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = false,
                WinnerId = 0
            };

            // Act

            var testingCode = () => sut.EndGameAsync(gameResultDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task Call_SaveChanges()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var handler = new Mock<Func<GameResultDto, Task>>();
            sut.GameEndAsync += handler.Object;

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = true,
                WinnerId = 0
            };

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.EndGameAsync(gameResultDto);

            // Assert

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }

        [Fact]
        public async Task Invoke_GameEndAsync()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var handler = new Mock<Func<GameResultDto, Task>>();
            sut.GameEndAsync += handler.Object;

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = true,
                WinnerId = 0
            };

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.EndGameAsync(gameResultDto);

            // Assert

            handler.Verify(h => h.Invoke(gameResultDto));
        }

        [Fact]
        public async Task Assign_WinnerAndLoser_WhenNotTied()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = false,
                WinnerId = 1
            };

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.EndGameAsync(gameResultDto);

            //Assert

            Assert.True(_game.WinnerId.HasValue && _game.LoserId.HasValue);
            //Assert.Equal(_game.WinnerId, gameResultDto.WinnerId);
            Assert.True(_game.LoserId > 0);
        }

        [Fact]
        public async Task Assign_NoWinnerAndLoser_WhenTied()
        {
            // Arrange

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                null,
                _moveServiceMock.Object,
                null);

            var gameResultDto = new GameResultDto
            {
                GameId = 1,
                IsTie = true,
                WinnerId = 0
            };

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);

            // Act

            await sut.EndGameAsync(gameResultDto);

            //Assert

            Assert.False(_game.WinnerId.HasValue && _game.LoserId.HasValue);
            Assert.True(_game.IsTied);
        }
    }
}
