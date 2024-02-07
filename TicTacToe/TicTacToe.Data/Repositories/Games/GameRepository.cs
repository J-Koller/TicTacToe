using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Games
{
    public class GameRepository : DatabaseRepositoryBase<Game>, IGameRepository
    {
        public GameRepository(TicTacToeContext ticTacToeAppContext) : base(ticTacToeAppContext)
        {
        }

        public async Task<Game> GetGameByIdAsync(int Id)
        {
            if(Id < 1)
            {
                throw new ArgumentException("Game Id must be greater than zero.");
            }

            var game = await _ticTacToeAppContext.Games.SingleAsync(g => g.Id == Id);

            return game;
        }

        public async Task<IEnumerable<Game>> GetGamesLookingForPlayersAsync()
        {
            var gamesLookingForPlayers = await _ticTacToeAppContext.Games.Where(
                g => g.IsLookingForPlayers && 
                g.Players.Count < 2)
                .ToListAsync();

            return gamesLookingForPlayers;
        }

        public async Task<IEnumerable<Game>> GetActiveGamesAsync()
        {
            var activeGames = await _ticTacToeAppContext.Games.Where(g => g.FinishDate == null).ToListAsync();

            return activeGames;
        }
    }
}
