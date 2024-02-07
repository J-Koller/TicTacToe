namespace TicTacToe.Api.Logic.Services.Players.Ranks
{
    public interface IRankService
    {
        string CalculateRank(int gamesWon, int gamesLost);
    }
}
