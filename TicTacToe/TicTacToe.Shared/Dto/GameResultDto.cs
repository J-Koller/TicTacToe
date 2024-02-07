using System.Collections.Generic;

namespace TicTacToe.Api.Shared.Dto
{
    public class GameResultDto
    {
        public int GameId { get; set; }

        public bool IsTie { get; set; }

        public int WinnerId { get; set; }

        public List<string> GameConnectionIds { get; set; }
    }
}
