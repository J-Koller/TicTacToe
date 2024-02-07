using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Players
{
    public class GetPlayerByIdAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetPlayerByIdAsync_Should()
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

            var sut = new PlayerRepository(_context);

            // Act

            var testingCode =  () => sut.GetPlayerByIdAsync(id);

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

            await _context.Players.AddAsync(expectedPlayer);
            await _context.SaveChangesAsync();


            var sut = new PlayerRepository(_context);

            // Act

            var actualPlayer = await sut.GetPlayerByIdAsync(1);

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

            await _context.Players.AddAsync(expectedPlayer);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            var actualPlayer = await sut.GetPlayerByIdAsync(1);

            // Assert

            Assert.Equal(expectedPlayer, actualPlayer);
        }
    }
}
