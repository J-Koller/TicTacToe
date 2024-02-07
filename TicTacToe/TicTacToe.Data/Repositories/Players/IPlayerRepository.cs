using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Players
{
    public interface IPlayerRepository : IRepositoryBase<Player>
    {
        Task<bool> UsernameExistsAsync(string username);

        Task<bool> PlayerExistsAsync(PlayerCredentialsDto playerCredentialsDto);

        Task<Player> GetPlayerByCredentialsAsync(PlayerCredentialsDto playerCredentialsDto);

        Task<Player> GetPlayerByIdAsync(int playerId);

        Task<int> GetGamesWonAsync(int playerId);

        Task<int> GetGamesLostAsync(int playerId);

        Task<IEnumerable<Player>> GetInactivePlayersAsync();
    }
}
