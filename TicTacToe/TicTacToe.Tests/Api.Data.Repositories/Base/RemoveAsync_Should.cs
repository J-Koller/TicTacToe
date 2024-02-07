using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;
using Xunit;
using System.Linq;

namespace TicTacToe.Tests.Api.Data.Repositories.Base
{
    public class RemoveAsync_Should
    {

        private readonly TicTacToeContext _context;

        public RemoveAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task ThrowIf_ArgumentIs_Null()
        {
            // Arrange

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            var testingCode = () => sut.RemoveAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }


        [Fact]
        public async Task RemoveEntity()
        {
            // Arrange

            var playerOne = new Player
            {
                Id = 1,
                Username = "username1",
                Password = "password1"
            };

            var playerTwo = new Player
            {
                Id = 2,
                Username = "username2",
                Password = "password2"
            };

            await _context.AddAsync(playerOne);
            await _context.AddAsync(playerTwo);
            await _context.SaveChangesAsync();

            int originalPlayerCount = _context.Players.Count();

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            await sut.RemoveAsync(playerTwo);

            // Assert

            Assert.NotEqual(originalPlayerCount, _context.Players.Count());
        }
    }
}
