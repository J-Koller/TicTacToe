using AutoMapper;
using Moq;
using System.Threading.Tasks;
using System;
using TicTacToe.Api.Logic.Services.Players.Ranks;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Logic.Services.Strings;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class RegisterNewPlayerAsync_Should
    {
        private readonly PlayerCredentialsDto _validPlayerCredentialsDto = new PlayerCredentialsDto
        {
            Username = "valid",
            Password = "credentials"
        };

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new PlayerService(null, null, null, null);

            // Act

            var testingCode = () => sut.RegisterNewPlayerAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Theory]
        [InlineData(null, "myPassword")]
        [InlineData("myUsername", null)]
        public async Task ThrowIf_ArgumentProperties_AreNull(string username, string password)
        {
            // Arrange

            var playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = username,
                Password = password
            };

            var sut = new PlayerService(null, null, null, null);

            // Act

            var testingCode = () => sut.RegisterNewPlayerAsync(playerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Credentials_AreNotAlphaNumeric()
        {
            // Arrange

            var bunkCredentialsDto = new PlayerCredentialsDto
            {
                Username = "!",
                Password = " $ "
            };

            var stringServiceMock = new Mock<IStringService>();
            stringServiceMock.Setup(ssm => ssm.IsAlphaNumeric(bunkCredentialsDto.Username, bunkCredentialsDto.Password)).Returns(false);

            var sut = new PlayerService(null, stringServiceMock.Object, null, null);

            // Act

            var testingCode = () => sut.RegisterNewPlayerAsync(bunkCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Username_Exists()
        {
            // Arrange

            var stringServiceMock = new Mock<IStringService>();
            stringServiceMock.Setup(ssm => ssm.IsAlphaNumeric(_validPlayerCredentialsDto.Username, _validPlayerCredentialsDto.Password)).Returns(true);

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(prm => prm.UsernameExistsAsync(_validPlayerCredentialsDto.Username)).ReturnsAsync(true);

            var sut = new PlayerService(playerRepositoryMock.Object, stringServiceMock.Object, null, null);

            // Act

            var testingCode = () => sut.RegisterNewPlayerAsync(_validPlayerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<InvalidOperationException>(testingCode);
        }

        [Fact]
        public async Task Save_NewPlayer_ToDatabase()
        {
            // Arrange

            var expectedPlayer = new Player();

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(prm => prm.UsernameExistsAsync(_validPlayerCredentialsDto.Username)).ReturnsAsync(false);

            var stringServiceMock = new Mock<IStringService>();
            stringServiceMock.Setup(ssm => ssm.IsAlphaNumeric(_validPlayerCredentialsDto.Username, _validPlayerCredentialsDto.Password)).Returns(true);

            var rankServiceMock = new Mock<IRankService>();
            rankServiceMock.Setup(rsm => rsm.CalculateRank(1, 2)).Returns("a new rank");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mm => mm.Map<Player>(_validPlayerCredentialsDto)).Returns(expectedPlayer);

            var sut = new PlayerService(playerRepositoryMock.Object, stringServiceMock.Object, rankServiceMock.Object, mapperMock.Object);

            // Act

            await sut.RegisterNewPlayerAsync(_validPlayerCredentialsDto);

            // Assert

            playerRepositoryMock.Verify(prm => prm.AddAsync(expectedPlayer));
        }
    }
}
