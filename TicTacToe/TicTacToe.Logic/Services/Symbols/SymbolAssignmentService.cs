using System.Threading.Tasks;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Symbols;

namespace TicTacToe.Api.Logic.Services.Symbols;

public class SymbolAssignmentService : ISymbolAssignmentService
{
    private readonly ISymbolRepository _symbolRepository;

    public SymbolAssignmentService(ISymbolRepository symbolRepository)
    {
        _symbolRepository = symbolRepository;
    }

    public async Task<Symbol> AssignPlayerOneSymbolAsync()
    {
        var symbol = await _symbolRepository.GetSymbolByIdAsync(1);

        return symbol;
    }

    public async Task<Symbol> AssignPlayerTwoSymbolAsync()
    {
        var symbol = await _symbolRepository.GetSymbolByIdAsync(2);

        return symbol;
    }
}
