using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Symbols
{
    public class SymbolRepository : DatabaseRepositoryBase<Symbol>, ISymbolRepository
    {
        public SymbolRepository(TicTacToeContext ticTacToeAppContext) : base(ticTacToeAppContext)
        {
        }

        public async Task<Symbol> GetSymbolByIdAsync(int Id)
        {
            if (Id < 1)
            {
                throw new ArgumentException("Id cannot be less than one.");
            }

            var symbol = await _ticTacToeAppContext.Symbols.SingleAsync(s => s.Id == Id);

            return symbol;
        }
    }
}
