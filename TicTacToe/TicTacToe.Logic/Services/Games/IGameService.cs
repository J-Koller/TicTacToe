using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Services.Games
{
    public interface IGameService
    {
        event Func<GameResultDto, Task> GameEndAsync;

        Task<IEnumerable<AvailableGameDto>> GetAvailableGamesAsync();

        Task<int> StartNewGameAsync(NewGameDto newGameDto);

        Task<PlayerDto> JoinGameAsync(JoinGameDto joinGameDto);

        Task<MoveDto> MakeMoveAsync(NewMoveDto newMoveDto);

        Task EndGameAsync(GameResultDto gameResultDto);

        Task AbandonGameAsync(GameResultDto gameResultDto);

        Task CleanGamesAsync();
    }
}
