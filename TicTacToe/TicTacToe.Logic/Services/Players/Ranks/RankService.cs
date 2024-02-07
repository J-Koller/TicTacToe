using System;

namespace TicTacToe.Api.Logic.Services.Players.Ranks
{
    public class RankService : IRankService
    {
        public string CalculateRank(int gamesWon, int gamesLost)
        {
            if (gamesWon < 0 || gamesLost < 0)
            {
                throw new ArgumentException("Games won or lost cannot be negative");
            }

            string newRank = "";

            if (gamesLost > gamesWon)
            {
                newRank = "Average";

                if ((gamesLost - gamesWon) > 10)
                {
                    newRank = "Frequent Loser";
                }
            }

            if (gamesWon == gamesLost)
            {
                newRank = "Even Steven";
            }

            if (gamesWon > gamesLost)
            {
                newRank = "Average";

                if ((gamesWon - gamesLost) > 10)
                {
                    newRank = "Frequent Winner";

                    if ((gamesWon - gamesLost) > 20)
                    {
                        newRank = "Tic-Tac-Toe God";
                    }
                }
            }
            return newRank;
        }
    }
}
