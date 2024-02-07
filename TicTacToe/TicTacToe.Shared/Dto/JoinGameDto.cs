namespace TicTacToe.Api.Shared.Dto
{
    public class JoinGameDto
    {
        public int PlayerId { get; set; }
        
        public int GameId { get; set; }

        public string GameHubConnectionId { get; set; }
    }
}
