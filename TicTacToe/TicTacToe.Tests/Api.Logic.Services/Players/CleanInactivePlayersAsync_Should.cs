using Moq;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class CleanInactivePlayersAsync_Should
    {
        Mock<IPlayerRepository> _playerRepositoryMock;

        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        private readonly Player _playerThree;
        private readonly List<Player> _expectedPlayers;

        public CleanInactivePlayersAsync_Should()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();

            _playerOne = new Player
            {
                IsLoggedIn = true
            };

            _playerTwo = new Player
            {
                IsLoggedIn = true
            };

            _playerThree = new Player
            {
                IsLoggedIn = true
            };

            _expectedPlayers = new List<Player> { _playerOne, _playerTwo, _playerThree };
        }

        [Fact]
        public async void Should_SetPlayerProperty_IsLoggedIn_To_False()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetInactivePlayersAsync()).ReturnsAsync(_expectedPlayers);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            await sut.CleanInactivePlayersAsync();

            // Assert

            bool playersAreLoggedOut = _expectedPlayers.All(p => p.IsLoggedIn == false);

            Assert.True(playersAreLoggedOut);
        }


        [Fact]
        public async void Should_Call_SaveChangesAsync()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetInactivePlayersAsync()).ReturnsAsync(_expectedPlayers);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            await sut.CleanInactivePlayersAsync();

            // Assert

            _playerRepositoryMock.Verify(prm => prm.SaveChangesAsync());
        }
    }
}
