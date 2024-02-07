
namespace TicTacToe.Api.Shared.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public string GameHubConnectionId { get; set; }

        public string Rank { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int GamesTied { get; set; }

        public int GamesPlayed => GamesWon + GamesLost + GamesTied;
    }
}
