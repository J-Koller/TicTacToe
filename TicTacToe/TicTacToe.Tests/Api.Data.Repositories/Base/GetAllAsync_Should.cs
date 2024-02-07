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
    public class GetAllAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetAllAsync_Should()
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

            var player = new Player
            {
                Id = 1,
                Username = "username",
                Password = "password"
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            var actualPlayers = await sut.GetAllAsync();

            // Assert

            Assert.NotNull(actualPlayers);
        }

        [Fact]
        public async Task Return_AllEntities()
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

            await _context.Players.AddAsync(playerOne);
            await _context.Players.AddAsync(playerTwo);
            await _context.SaveChangesAsync();

            int expectedPlayerCount = _context.Players.Count();

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            var actualPlayers = await sut.GetAllAsync();

            // Assert

            Assert.Equal(expectedPlayerCount, actualPlayers.Count());
        }
    }
}
