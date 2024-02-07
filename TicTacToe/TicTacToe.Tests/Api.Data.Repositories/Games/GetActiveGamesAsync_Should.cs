using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;
using System.Linq;

namespace TicTacToe.Tests.Api.Data.Repositories.Games
{
    public class GetActiveGamesAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetActiveGamesAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new GameRepository(_context);

            // Act

            var activeGames = await sut.GetActiveGamesAsync();

            // Assert

            Assert.NotNull(activeGames);
        }

        [Fact]
        public async Task Return_ExpectedGames()
        {
            // Arrange

            Game gameOne = new Game
            {
                FinishDate = null
            };

            Game gameTwo = new Game
            {
                FinishDate = null
            };

            Game gameThree = new Game
            {
                FinishDate = DateTime.Now
            };

            await _context.Games.AddRangeAsync(gameOne, gameTwo, gameThree);
            await _context.SaveChangesAsync();

            var sut = new GameRepository(_context);

            // Act

            var activeGames = await sut.GetActiveGamesAsync();

            // Assert

            Assert.Equal(2, activeGames.Count());
        }
    }
}
