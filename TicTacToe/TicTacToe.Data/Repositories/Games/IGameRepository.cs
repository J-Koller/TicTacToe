using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Games
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<Game> GetGameByIdAsync(int Id);

        Task<IEnumerable<Game>> GetGamesLookingForPlayersAsync();

        Task<IEnumerable<Game>> GetActiveGamesAsync();
    }
}
