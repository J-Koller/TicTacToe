using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Services.Moves
{
    public interface IMoveService
    {
        event Func<GameResultDto, Task> MatchingSequenceAsync;

        Task AddMoveAsync(List<MoveDto> moveDtos, int gameId);
    }
}
