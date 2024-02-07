using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Base
{
    public class AddAsync_Should
    {
        private readonly TicTacToeContext _context;

        public AddAsync_Should()
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

            var testingCode = () => sut.AddAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task NotReturn_LessThanOne()
        {
            // Arrange

            var player = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            int playerId = await sut.AddAsync(player);

            // Assert

            Assert.True(playerId > 0);
        }

        [Fact]
        public async Task AddEntity()
        {
            // Arrange

            var player = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            int playerId = await sut.AddAsync(player);

            // Assert

            Assert.Equal(_context.Players.First(), player);
        }
    }
}
