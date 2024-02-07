using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Games
{
    public class GetGamesLookingForPlayersAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetGamesLookingForPlayersAsync_Should()
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

            var availableGames = await sut.GetGamesLookingForPlayersAsync();

            // Assert

            Assert.NotNull(availableGames);
        }

        [Fact]
        public async Task Return_ExpectedGames()
        {
            // Arrange

            Player playerOne = new Player
            {
                Id = 1,
                Username = "a",
                Password = "a"
            };

            Player playerTwo = new Player
            {
                Id = 2,
                Username = "b",
                Password = "b"
            };

            Game gameOne = new Game
            {
                Id = 1,
                IsLookingForPlayers = true,
            };

            Game gameTwo = new Game
            {
                Id = 2,
                IsLookingForPlayers = true
            };

            Game fullGame = new Game
            {
                Id = 3,
                IsLookingForPlayers = false
            };

            gameOne.Players.Add(playerOne);
            gameTwo.Players.Add(playerTwo);

            await _context.Games.AddRangeAsync(gameOne, gameTwo, fullGame);
            await _context.SaveChangesAsync();

            var sut = new GameRepository(_context);

            // Act

            var availableGames = await sut.GetGamesLookingForPlayersAsync();

            // Assert

            Assert.Equal(2, availableGames.Count());
        }
    }
}
