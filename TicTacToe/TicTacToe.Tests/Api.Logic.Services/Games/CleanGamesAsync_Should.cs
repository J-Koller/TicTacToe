using Moq;
using System;
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
    public class CleanGamesAsync_Should
    {
        Mock<IGameRepository> _gameRepositoryMock;
        Mock<IMoveService> _moveServiceMock;

        private readonly Player _loggedInPlayer;
        private readonly Player _loggedOutPlayer;

        private readonly Game _gameOne;
        private readonly Game _gameTwo;
        private readonly Game _gameThree;
        private readonly Game _gameFour;
        private readonly Game _gameFive;

        private readonly List<Game> _expectedActiveGames;
        private readonly List<Game> _expectedGamesWithLoggedOutPlayers;
        private readonly List<Game> _expectedFullGamesWithSingleLoggedOutPlayer;

        public CleanGamesAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _moveServiceMock = new Mock<IMoveService>();

            _loggedInPlayer = new Player { Id = 1, IsLoggedIn = true};
            _loggedOutPlayer = new Player { IsLoggedIn = false };

            _gameOne = new Game { IsLookingForPlayers = true };
            _gameTwo = new Game();
            _gameThree = new Game();
            _gameFour = new Game();
            _gameFive = new Game { IsLookingForPlayers = true };

            _expectedActiveGames = new List<Game>();
            _expectedGamesWithLoggedOutPlayers = new List<Game>();
            _expectedFullGamesWithSingleLoggedOutPlayer = new List<Game>();
        }

        [Fact]
        public async void OnlySet_GameProperty_FinishDate_ToNow_OnValidGames()
        {
            // Arrange

            ArrangeData();

            _gameRepositoryMock.Setup(grm => grm.GetActiveGamesAsync()).ReturnsAsync(_expectedActiveGames);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            await sut.CleanGamesAsync();

            // Assert

            Assert.False(_expectedActiveGames.All(g => g.FinishDate.HasValue));
            Assert.True(_expectedGamesWithLoggedOutPlayers.All(g => g.FinishDate.HasValue));
        }

        [Fact]
        public async void OnlySet_GameProperty_IsLookingForPlayers_ToFalse_OnValidGames()
        {
            // Arrange

            ArrangeData();

            _gameRepositoryMock.Setup(grm => grm.GetActiveGamesAsync()).ReturnsAsync(_expectedActiveGames);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            await sut.CleanGamesAsync();

            // Assert

            Assert.False(_expectedActiveGames.All(g => !g.IsLookingForPlayers));
            Assert.True(_expectedGamesWithLoggedOutPlayers.All(g => !g.IsLookingForPlayers));
        }

        [Fact]
        public async void Invoke_GameEndAsync_WithCorrectData_ForValidGames()
        {
            // Arrange
            
            ArrangeData();

            _gameRepositoryMock.Setup(grm => grm.GetActiveGamesAsync()).ReturnsAsync(_expectedActiveGames);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            var handler = new Mock<Func<GameResultDto, Task>>();
            sut.GameEndAsync += handler.Object;

            GameResultDto capturedGameResultDto = null;

            handler.Setup(h => h.Invoke(It.IsAny<GameResultDto>()))
                .Callback<GameResultDto>(gameResultDto => capturedGameResultDto = gameResultDto);

            // Act

            await sut.CleanGamesAsync();

            // Assert

            handler.Verify(h => h.Invoke(It.IsAny<GameResultDto>()));

            Assert.True(capturedGameResultDto.WinnerId == _gameThree.Winner.Id);
            Assert.True(capturedGameResultDto.IsTie == false);
            Assert.Contains(_gameThree.PlayerOneGameConnectionId, capturedGameResultDto.GameConnectionIds);
        }

        [Fact]
        public async void Set_GameProperty_WinnerAndLoser_OnValidGames()
        {
            // Arrange

            ArrangeData();

            _gameRepositoryMock.Setup(grm => grm.GetActiveGamesAsync()).ReturnsAsync(_expectedActiveGames);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            await sut.CleanGamesAsync();

            // Assert

            Assert.True(_gameThree.Winner == _loggedInPlayer);
            Assert.True(_gameThree.Loser == _loggedOutPlayer);
        }

        [Fact]
        public async void Call_SaveChangesAsync()
        {
            // Arrange

            ArrangeData();

            _gameRepositoryMock.Setup(grm => grm.GetActiveGamesAsync()).ReturnsAsync(_expectedActiveGames);

            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            await sut.CleanGamesAsync();

            // Assert

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }

        private void ArrangeData()
        {
            // game one: 1 player who is logged out
            _gameOne.Players.Add(_loggedOutPlayer);

            // game two: 2 players, both logged out
            _gameTwo.Players.Add(_loggedOutPlayer);
            _gameTwo.Players.Add(_loggedOutPlayer);

            // game three: 2 players, one logged out
            _gameThree.Players.Add(_loggedInPlayer);
            _gameThree.Players.Add(_loggedOutPlayer);
            _gameThree.PlayerOneGameConnectionId = "connectionId";
            _gameThree.PlayerOneId = _loggedInPlayer.Id;

            // game four: normal game
            _gameFour.Players.Add(_loggedInPlayer);
            _gameFour.Players.Add(_loggedInPlayer);

            // game five: active game, looking for players, with one active player
            _gameFive.Players.Add(_loggedInPlayer);
            
            _expectedActiveGames.AddRange(new List<Game> { _gameOne, _gameTwo, _gameThree, _gameFour, _gameFive });
            
            _expectedGamesWithLoggedOutPlayers.AddRange(new List<Game> { _gameOne, _gameTwo, _gameThree });

            _expectedFullGamesWithSingleLoggedOutPlayer.Add(_gameThree);
        }
    }
}