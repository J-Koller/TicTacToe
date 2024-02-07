using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Players
{
    public class GetGamesLostAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetGamesLostAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task ThrowIf_ArgumentIs_LessThanOne()
        {
            // Arrange

            var sut = new PlayerRepository(_context);

            // Act

            var testingCode = () => sut.GetGamesLostAsync(0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task NotReturn_Negative()
        {
            // Arrange

            var player = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            int gamesLost = await sut.GetGamesLostAsync(1);

            // Assert

            Assert.True(gamesLost >= 0);
        }

        [Fact]
        public async Task Return_ExpectedInt()
        {
            // Arrange

            var player = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            int expectedLoses = player.Games.Count(g => g.WinnerId == player.Id);

            var sut = new PlayerRepository(_context);

            // Act

            int actualLoses = await sut.GetGamesLostAsync(1);

            // Assert

            Assert.Equal(expectedLoses, actualLoses);
        }
    }
}
