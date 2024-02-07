using System.Threading.Tasks;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Symbols
{
    public interface ISymbolRepository : IRepositoryBase<Symbol>
    {
        Task<Symbol> GetSymbolByIdAsync(int Id);
    }
}
