using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Games
{
    public class GetGameByIdAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetGameByIdAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ThrowIf_ArgumentIs_LessThanOne(int id)
        {
            // Arrange

            var sut = new GameRepository(_context);

            // Act

            var testingCode = () => sut.GetGameByIdAsync(id);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var expectedGame = new Game
            {
                Id = 1,
            };

            await _context.Games.AddAsync(expectedGame);
            await _context.SaveChangesAsync();


            var sut = new GameRepository(_context);

            // Act

            var actualGame = await sut.GetGameByIdAsync(1);

            // Assert

            Assert.NotNull(actualGame);
        }

        [Fact]
        public async Task Return_ExpectedGame()
        {
            // Arrange

            var expectedGame = new Game
            {
                Id = 1,
            };

            await _context.Games.AddAsync(expectedGame);
            await _context.SaveChangesAsync();

            var sut = new GameRepository(_context);

            // Act

            var actualGame = await sut.GetGameByIdAsync(1);

            // Assert

            Assert.Equal(expectedGame, actualGame);
        }
    }
}
