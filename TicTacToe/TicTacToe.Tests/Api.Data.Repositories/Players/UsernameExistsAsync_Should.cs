using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Players
{
    public class UsernameExistsAsync_Should
    {
        private readonly TicTacToeContext _context;

        public UsernameExistsAsync_Should()
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

            var testingCode = () => sut.UsernameExistsAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsEmpty()
        {
            // Arrange

            var sut = new PlayerRepository(_context);

            // Act

            var testingCode = () => sut.UsernameExistsAsync("");

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task Return_True_WhenName_Exists()
        {
            // Arrange

            var player = new Player
            {
                Username = "test username",
                Password = "test password"
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            bool usernameExists = await sut.UsernameExistsAsync("test username");

            // Assert

            Assert.True(usernameExists);
        }

        [Fact]
        public async Task Return_False_WhenName_DoesNotExist()
        {
            // Arrange

            var player = new Player
            {
                Username = "test username",
                Password = "test password"
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            bool usernameExists = await sut.UsernameExistsAsync("something else");

            // Assert

            Assert.False(usernameExists);
        }
    }
}
