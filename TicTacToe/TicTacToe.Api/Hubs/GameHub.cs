using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Hubs
{
    public class GameHub : Hub
    {

        public async Task PlayerJoin(PlayerDto joiningPlayerDto, params string[] connectionIds)
        {
            await Clients.Clients(connectionIds).SendAsync("RecievedPlayer", joiningPlayerDto);
        }

        public async Task AddMove(MoveDto moveDto, params string[] connectionIds)
        {
            await Clients.Clients(connectionIds).SendAsync("RecievedMove", moveDto);
        }

        public async Task AbandonGame(PlayerDto quittingPlayerDto, params string[] connectionIds)
        {
            await Clients.Clients(connectionIds).SendAsync("RecievedQuitter", quittingPlayerDto);
        }
    }
}
