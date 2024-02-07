using System.Threading.Tasks;
using TicTacToe.Data.Entities;

namespace TicTacToe.Api.Logic.Services.Symbols
{
    public interface ISymbolAssignmentService
    {
        Task<Symbol> AssignPlayerOneSymbolAsync();

        Task<Symbol> AssignPlayerTwoSymbolAsync();
    }
}
