using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Players
{
    public class GetPlayerByCredentialsAsync_Should
    {
        private readonly TicTacToeContext _context;
        
        public GetPlayerByCredentialsAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new PlayerRepository(_context);

            // Act

            var testingCode = () => sut.GetPlayerByCredentialsAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Theory]
        [InlineData("", "not empty")]
        [InlineData("not empty", "")]
        [InlineData(null, "not null")]
        [InlineData("not null", null)]
        public async Task ThrowIf_ArgumentProperties_AreNullOrEmpty(string username, string password)
        {
            // Arrange

            var playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = username,
                Password = password
            };

            var sut = new PlayerRepository(_context);

            // Act

            var testingCode = () => sut.GetPlayerByCredentialsAsync(playerCredentialsDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var expectedPlayer = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            var playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = "username",
                Password = "password"
            };

            await _context.Players.AddAsync(expectedPlayer);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            var actualPlayer = await sut.GetPlayerByCredentialsAsync(playerCredentialsDto);

            // Assert

            Assert.NotNull(actualPlayer);
        }

        [Fact]
        public async Task Return_ExpectedPlayer()
        {
            // Arrange

            var expectedPlayer = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            var playerCredentialsDto = new PlayerCredentialsDto
            {
                Username = "username",
                Password = "password"
            };

            await _context.Players.AddAsync(expectedPlayer);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            var actualPlayer = await sut.GetPlayerByCredentialsAsync(playerCredentialsDto);

            // Assert

            Assert.Equal(expectedPlayer, actualPlayer);
        }
    }
}
