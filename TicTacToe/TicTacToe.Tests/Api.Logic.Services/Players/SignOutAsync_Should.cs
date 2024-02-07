using Moq;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class SignOutAsync_Should
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;
        private readonly Player _expectedPlayer;

        public SignOutAsync_Should()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();

            _expectedPlayer = new Player { Id = 1, IsLoggedIn = true };
        }

        [Fact]
        public async Task ThrowIf_Argument_IsLessThanOne()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1))
                .ReturnsAsync(_expectedPlayer);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var testingCode = () => sut.SignOutAsync(0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task Assign_FalseTo_Player_IsLoggedIn_AndSave()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1))
                .ReturnsAsync(_expectedPlayer);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            await sut.SignOutAsync(1);

            // Assert

            Assert.False(_expectedPlayer.IsLoggedIn);
            _playerRepositoryMock.Verify(prm => prm.SaveChangesAsync());
        }
    }
}
