namespace TicTacToe.Api.Shared.Dto
{
    public class NewMoveDto
    {
        public int PlayerId { get; set; }

        public int GameId { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }
    }
}
