using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Services.Players
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayerDtosAsync();

        Task RegisterNewPlayerAsync(PlayerCredentialsDto playerCredentialsDto);

        Task<int> SignInAsync(PlayerCredentialsDto playerCredentialsDto);

        Task SignOutAsync(int playerId);

        Task<PlayerDto> GetPlayerDtoAsync(int playerId);

        Task PlayerHeartBeatAsync(int playerId);

        Task CleanInactivePlayersAsync();
    }
}
