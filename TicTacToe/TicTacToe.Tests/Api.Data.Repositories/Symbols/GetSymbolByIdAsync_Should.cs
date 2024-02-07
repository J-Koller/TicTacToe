using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;
using TicTacToe.Data.Repositories.Symbols;
using Xunit;

namespace TicTacToe.Tests.Api.Data.Repositories.Symbols
{
    public class GetSymbolByIdAsync_Should
    {
        private readonly TicTacToeContext _context;

        public GetSymbolByIdAsync_Should()
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

            var symbol = new Symbol
            {
                Id = 1,
                Value = "X"
            };

            await _context.Symbols.AddAsync(symbol);
            await _context.SaveChangesAsync();

            var sut = new SymbolRepository(_context);

            // Act

            var actualSymbol = await sut.GetSymbolByIdAsync(1);

            // Assert

            Assert.NotNull(actualSymbol);
        }

        [Fact]
        public async Task Return_Expected_Symbol()
        {
            // Arrange

            var expectedSymbol = new Symbol
            {
                Id = 1,
                Value = "X"
            };

            await _context.Symbols.AddAsync(expectedSymbol);
            await _context.SaveChangesAsync();

            var sut = new SymbolRepository(_context);

            // Act

            var actualSymbol = await sut.GetSymbolByIdAsync(1);

            // Assert

            Assert.Equal(expectedSymbol, actualSymbol);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsLessThan_One()
        {
            // Arrange

            var sut = new SymbolRepository(_context);

            // Act

            var testingCode = () => sut.GetSymbolByIdAsync(0);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }
    }
}
