using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Base
{
    public class SaveChangesAsync_Should
    {
        private readonly TicTacToeContext _context;

        private bool _savedChangesEventRaised = false;

        public SaveChangesAsync_Should()
        {
            string databaseName = Guid.NewGuid().ToString();

            DbContextOptionsBuilder<TicTacToeContext> optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>()
                .UseInMemoryDatabase(databaseName);

            _context = new TicTacToeContext(optionsBuilder.Options);

            _context.SavedChanges += OnSavedChanges;
        }

        private void OnSavedChanges(object sender, EventArgs e)
        {
            _savedChangesEventRaised = true;
        }

        [Fact]
        public async Task SaveChanges()
        {
            // Arrange

            var sut = new DatabaseRepositoryBase<Player>(_context);

            // Act

            await sut.SaveChangesAsync();

            // Assert

            Assert.True(_savedChangesEventRaised);
        }
    }
}
