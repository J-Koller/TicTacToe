using System.Drawing;

namespace TicTacToe.Api.Shared.Dto
{
    public class MoveDto
    {
        public string Symbol { get; set; }

        public int PlayerId { get; set; }

        public Point Coordinates { get; set; }
    }
}
