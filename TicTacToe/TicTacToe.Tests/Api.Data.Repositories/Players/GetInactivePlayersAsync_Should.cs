using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Players
{
    public class GetInactivePlayersAsync_Should
    {
        private readonly TicTacToeContext _context;

        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        private readonly Player _playerThree;

        private readonly List<Player> _expectedPlayers;

        private readonly DateTime _fourtySecondsAgo;
        
        public GetInactivePlayersAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);

            _playerOne = new Player
            {
                Username = "Sebby",
                Password = "Szafran",
                IsLoggedIn = false,
            };

            _playerTwo = new Player
            {
                Username = "Jared",
                Password = "Koller",
                IsLoggedIn = true,
                LastActive = DateTime.UtcNow
            };

            _playerThree = new Player
            {
                Username = "Stephen",
                Password = "Kogot",
                IsLoggedIn = true,
                LastActive = _fourtySecondsAgo
            };

            _fourtySecondsAgo = DateTime.UtcNow.AddSeconds(-40);

            _expectedPlayers = new List<Player> { _playerThree };
        }

        [Fact]
        public async void NotReturn_Null()
        {
            // Arrange

            await _context.Players.AddAsync(_playerOne);
            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            var inactivePlayers = await sut.GetInactivePlayersAsync();

            // Assert

            Assert.NotNull(inactivePlayers);
        }

        [Fact]
        public async void Return_ExpectedValue()
        {
            // Arrange

            await _context.Players.AddAsync(_playerOne);
            await _context.Players.AddAsync(_playerTwo);
            await _context.Players.AddAsync(_playerThree);

            await _context.SaveChangesAsync();

            var sut = new PlayerRepository(_context);

            // Act

            var inactivePlayers = await sut.GetInactivePlayersAsync();

            // Assert

            Assert.Equal(_expectedPlayers.Count, inactivePlayers.Count());
        }
    }
}
