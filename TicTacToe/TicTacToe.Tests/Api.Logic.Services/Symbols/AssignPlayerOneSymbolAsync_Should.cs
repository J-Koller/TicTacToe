using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Strings;
using TicTacToe.Api.Logic.Services.Symbols;
using TicTacToe.Data.Repositories.Symbols;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Symbols
{
    public class AssignPlayerOneSymbolAsync_Should
    {
        private readonly Mock<ISymbolRepository> _symbolRepositoryMock;

        public AssignPlayerOneSymbolAsync_Should()
        {
            _symbolRepositoryMock = new Mock<ISymbolRepository>();
        }

        [Fact]
        public void NotReturn_Null()
        {
            // Arrange

            var sut = new SymbolAssignmentService(_symbolRepositoryMock.Object);

            // Act

            var symbol = sut.AssignPlayerOneSymbolAsync();

            // Assert

            Assert.NotNull(symbol);
        }
    }
}
