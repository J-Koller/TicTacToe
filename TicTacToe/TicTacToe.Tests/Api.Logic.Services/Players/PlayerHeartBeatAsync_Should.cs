using Moq;
using System;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class PlayerHeartBeatAsync_Should
    {
        Mock<IPlayerRepository> _playerRepositoryMock;
        
        private readonly Player _expectedPlayer;

        public PlayerHeartBeatAsync_Should()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();

            _expectedPlayer = new Player
            {
                Id = 1
            };
        }

        [Fact]
        public async void Should_ThrowIf_Argument_IsLessThanOne()
        {
            // Arrange

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var testingCode = async () => await sut.PlayerHeartBeatAsync(0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async void Should_SetPlayerProperty_LastActive_ToUtcNow()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_expectedPlayer);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            await sut.PlayerHeartBeatAsync(1);

            // Assert

            bool HeartBeatIsCloseToCurrentTime = (DateTime.UtcNow - _expectedPlayer.LastActive).TotalSeconds < 1;

            Assert.True(HeartBeatIsCloseToCurrentTime);
        }

        [Fact]
        public async void Should_Call_SaveChangesAsync()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_expectedPlayer);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            await sut.PlayerHeartBeatAsync(1);

            // Assert

            _playerRepositoryMock.Verify(prm => prm.SaveChangesAsync());
        }
    }
}
