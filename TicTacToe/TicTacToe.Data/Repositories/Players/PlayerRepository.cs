using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Configuration;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Base;

namespace TicTacToe.Data.Repositories.Players
{
    public class PlayerRepository : DatabaseRepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(TicTacToeContext ticTacToeAppContext) : base(ticTacToeAppContext)
        {
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            ArgumentException.ThrowIfNullOrEmpty(username);
            
            bool usernameExists = await _ticTacToeAppContext.Players
                .AnyAsync(p => p.Username == username);

            return usernameExists;
        }

        public async Task<bool> PlayerExistsAsync(PlayerCredentialsDto playerCredentialsDto)
        {
            ArgumentNullException.ThrowIfNull(playerCredentialsDto);

            if(playerCredentialsDto.Username.IsNullOrEmpty() || playerCredentialsDto.Password.IsNullOrEmpty())
            {
                throw new ArgumentException("Username or Password is null or empty.");
            }

            bool playerExists = await _ticTacToeAppContext.Players
                .AnyAsync(p => p.Username == playerCredentialsDto.Username && p.Password == playerCredentialsDto.Password);

            return playerExists;
        }
        
        public async Task<Player> GetPlayerByCredentialsAsync(PlayerCredentialsDto playerCredentialsDto)
        {
            ArgumentNullException.ThrowIfNull(playerCredentialsDto);

            if (playerCredentialsDto.Username.IsNullOrEmpty() || playerCredentialsDto.Password.IsNullOrEmpty())
            {
                throw new ArgumentException("Username or Password is null or empty.");
            }

            var player = await _ticTacToeAppContext.Players
                .Include(p => p.Games)
                .SingleAsync(p => p.Username == playerCredentialsDto.Username && p.Password == playerCredentialsDto.Password);

            return player;
        }

        public async Task<Player> GetPlayerByIdAsync(int playerId)
        {
            if(playerId < 1)
            {
                throw new ArgumentException("ID must be greater than 0.");
            }

            var player = await _ticTacToeAppContext.Players
                .Include(p => p.Games)
                .SingleAsync(p => p.Id == playerId);

            return player;
        }

        public async Task<int> GetGamesWonAsync(int playerId)
        {
            if (playerId < 1)
            {
                throw new ArgumentException("ID must be greater than 0.");
            }

            var gamesWon = await _ticTacToeAppContext.Players
                .Where(p => p.Id == playerId)
                .SelectMany(p => p.Games)
                .CountAsync(g => g.WinnerId == playerId);

            return gamesWon;
        }

        public async Task<int> GetGamesLostAsync(int playerId)
        {
            if (playerId < 1)
            {
                throw new ArgumentException("ID must be greater than 0.");
            }

            var gamesLost = await _ticTacToeAppContext.Players
                .Where(p => p.Id == playerId)
                .SelectMany(p => p.Games)
                .CountAsync(g => g.LoserId == playerId);

            return gamesLost;
        }

        public async Task<IEnumerable<Player>> GetInactivePlayersAsync()
        {
            var inactivePlayers = await _ticTacToeAppContext.Players
                .Where(p => p.LastActive < DateTime.UtcNow.AddSeconds(-15) && p.IsLoggedIn == true)
                .ToListAsync();

            return inactivePlayers;
        }
    }
}
