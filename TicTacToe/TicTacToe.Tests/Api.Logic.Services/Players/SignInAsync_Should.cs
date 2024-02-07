using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class SignInAsync_Should
    {
        private readonly Mock<IPlayerRepository> _playerRepositoryMock;

        private readonly PlayerCredentialsDto _playerCredentialsDto;
        private readonly Player _player;

        public SignInAsync_Should()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();

            _playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = "username credential",
                Password = "password credential"
            };

            _player = new Player
            {
                Id = 1,
                Username = "player username",
                Password = "player password",
                IsLoggedIn = false
            };
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new PlayerService(null, null, null, null);

            // Act

            var testingCode = () => sut.SignInAsync(null);

            // Assert
            
            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Theory]
        [InlineData(null, "myPassword")]
        [InlineData("myUsername", null)]
        public async Task ThrowIf_ArgumentProperties_AreNull(string username, string password)
        {
            // Arrange

            _playerCredentialsDto.Username = username;
            _playerCredentialsDto.Password = password;

            var sut = new PlayerService(null, null, null, null);

            // Act

            var testingCode = () => sut.SignInAsync(_playerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Player_NotFound()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.PlayerExistsAsync(_playerCredentialsDto))
                .ReturnsAsync(false);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var testingCode = () => sut.SignInAsync(_playerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task NotReturn_LessThanOne()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.PlayerExistsAsync(_playerCredentialsDto))
                .ReturnsAsync(true);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByCredentialsAsync(_playerCredentialsDto))
                .ReturnsAsync(_player);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var playerId = await sut.SignInAsync(_playerCredentialsDto);

            // Assert

            Assert.True(playerId > 0);
        }

        [Fact]
        public async Task ThrowIf_Player_IsLoggedIn()
        {
            // Arrange

            _player.IsLoggedIn = true;

            _playerRepositoryMock.Setup(prm => prm.PlayerExistsAsync(_playerCredentialsDto))
                .ReturnsAsync(true);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByCredentialsAsync(_playerCredentialsDto))
                .ReturnsAsync(_player);

            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var testingCode = () => sut.SignInAsync(_playerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<InvalidOperationException>(testingCode);
        }

        [Fact]
        public async Task Assign_TrueTo_Player_IsLoggedIn_AndSaves()
        {
            // Arrange

            _playerRepositoryMock.Setup(prm => prm.PlayerExistsAsync(_playerCredentialsDto))
                .ReturnsAsync(true);
            _playerRepositoryMock.Setup(prm => prm.GetPlayerByCredentialsAsync(_playerCredentialsDto))
                .ReturnsAsync(_player);


            var sut = new PlayerService(_playerRepositoryMock.Object, null, null, null);

            // Act

            var playerDto = await sut.SignInAsync(_playerCredentialsDto);

            // Assert

            Assert.True(_player.IsLoggedIn);
            _playerRepositoryMock.Verify(prm => prm.SaveChangesAsync());
        }
    }
}
