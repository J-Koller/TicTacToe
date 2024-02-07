using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Logic.Services.Players.Ranks;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players
{
    public class GetPlayerDtoAsync_Should
    {
        private readonly Player _player = new Player
        {
            Username = "A Player",
            Password = "A Password",
            Rank = "Unranked",
        };

        private readonly PlayerDto _playerDto = new PlayerDto
        {
            Username = "A PlayerDto"
        };

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_player);

            var rankServiceMock = new Mock<IRankService>();
            rankServiceMock.Setup(rsm => rsm.CalculateRank(1, 2)).Returns("a new rank");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mm => mm.Map<PlayerDto>(_player)).Returns(_playerDto);

            var sut = new PlayerService(playerRepositoryMock.Object, null, rankServiceMock.Object, mapperMock.Object);

            // Act

            var playerDto = await sut.GetPlayerDtoAsync(1);

            // Assert

            Assert.NotNull(playerDto);
        }

        [Fact]
        public async Task Save_UpdatedPlayer_ToDatabase()
        {
            // Arrange

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(prm => prm.GetPlayerByIdAsync(1)).ReturnsAsync(_player);

            var rankServiceMock = new Mock<IRankService>();
            rankServiceMock.Setup(rsm => rsm.CalculateRank(1, 2)).Returns("a new rank");

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mm => mm.Map<PlayerDto>(_player)).Returns(_playerDto);

            var sut = new PlayerService(playerRepositoryMock.Object, null, rankServiceMock.Object, mapperMock.Object);

            // Act

            var playerDto = await sut.GetPlayerDtoAsync(1);

            // Assert

            playerRepositoryMock.Verify(prm => prm.SaveChangesAsync());
        }

        [Fact]
        public async Task ThrowIf_Argument_IsLessThan_One()
        {
            // Arrange


            var sut = new PlayerService(null, null, null, null);

            // Act

            var testingCode = () => sut.GetPlayerDtoAsync(0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }
    }
}
