using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Players.Ranks;
using TicTacToe.Api.Logic.Services.Strings;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Players;

namespace TicTacToe.Api.Logic.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IStringService _stringService;
        private readonly IRankService _rankService;
        private readonly IMapper _mapper;

        private readonly string _playerIdLessThanOneMessage = "Player Id cannot be less than one.";
        private readonly string _credentialsEmptyMessage = "Please enter a username and a password.";
        private readonly string _usernameOrPassNullMessage = "Username/Password is null.";
        private readonly string _playerCredentialsDtoNullMessage = "PlayerCredentialsDto cannot be null.";
        private readonly string _usernameTakenMessage = "Username is already in use.";
        private readonly string _notAlphaNumericMessage = "Use only letters or numbers.";
        private readonly string _invalidSignInCredentialsMessage = "Incorrect Username or Password.";
        private readonly string _playerAlreadyLoggedInMessage = "Player is already logged in elsewhere. Please log out of other clients and try again.";

        public PlayerService(
            IPlayerRepository playerRepository,
            IStringService stringService,
            IRankService rankService,
            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _stringService = stringService;
            _rankService = rankService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayerDtosAsync()
        {
            var allPlayers = await _playerRepository.GetAllAsync();

            var allPlayerDtos = _mapper.Map<IEnumerable<PlayerDto>>(allPlayers);

            return allPlayerDtos;
        }

        public async Task RegisterNewPlayerAsync(PlayerCredentialsDto playerCredentialsDto)
        {
            //TODO update unit tests
            ArgumentNullException.ThrowIfNull(playerCredentialsDto, _playerCredentialsDtoNullMessage);

            if (playerCredentialsDto.Username is null || playerCredentialsDto.Password is null)
            {
                throw new ArgumentNullException(_usernameOrPassNullMessage);
            }

            if(playerCredentialsDto.Username == string.Empty || playerCredentialsDto.Password == string.Empty)
            {
                throw new ArgumentException(_credentialsEmptyMessage);
            }

            bool validCredentials = _stringService.IsAlphaNumeric(playerCredentialsDto.Username, playerCredentialsDto.Password);

            if (!validCredentials)
            {
                throw new ArgumentException(_notAlphaNumericMessage);
                
            }

            bool usernameExists = await _playerRepository.UsernameExistsAsync(playerCredentialsDto.Username);

            if (usernameExists)
            {
                throw new InvalidOperationException(_usernameTakenMessage);
            }

            var newPlayer = _mapper.Map<Player>(playerCredentialsDto);
            newPlayer.Rank = "Unranked";

            await _playerRepository.AddAsync(newPlayer);
        }

        public async Task<int> SignInAsync(PlayerCredentialsDto playerCredentialsDto)
        {
            ArgumentNullException.ThrowIfNull(playerCredentialsDto, _playerCredentialsDtoNullMessage);

            if (playerCredentialsDto.Username.IsNullOrEmpty() || playerCredentialsDto.Password.IsNullOrEmpty())
            {
                throw new ArgumentNullException(_usernameOrPassNullMessage);
            }

            var playerFound = await _playerRepository.PlayerExistsAsync(playerCredentialsDto);

            if (!playerFound)
            {
                throw new ArgumentException(_invalidSignInCredentialsMessage);
            }

            var player = await _playerRepository.GetPlayerByCredentialsAsync(playerCredentialsDto);

            if (player.IsLoggedIn)
            {
                throw new InvalidOperationException(_playerAlreadyLoggedInMessage);
            }
            
            player.IsLoggedIn = true;
            
            await _playerRepository.SaveChangesAsync();

            return player.Id;
        }

        public async Task SignOutAsync(int playerId)
        {
            if (playerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId);

            player.IsLoggedIn = false;

            await _playerRepository.SaveChangesAsync();
        }

        public async Task<PlayerDto> GetPlayerDtoAsync(int playerId)
        {
            if (playerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId);

            var updatedPlayer = await CalculateRankAsync(player);
                
            var playerDto = _mapper.Map<PlayerDto>(updatedPlayer);

            return playerDto;
        }

        private async Task<Player> CalculateRankAsync(Player player)
        {
            int gamesWon = player.Games.Where(g => g.WinnerId == player.Id).Count();
            int gamesLost = player.Games.Where(g => g.LoserId == player.Id).Count();

            player.Rank = _rankService.CalculateRank(gamesWon, gamesLost);

            await _playerRepository.SaveChangesAsync();

            return player;
        }

        public async Task PlayerHeartBeatAsync(int playerId)
        {
            if(playerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId);

            player.LastActive = DateTime.UtcNow;
            //TODO: update unit test
            player.IsLoggedIn = true;

            await _playerRepository.SaveChangesAsync();
        }

        public async Task CleanInactivePlayersAsync()
        {
            var inactivePlayers = await _playerRepository.GetInactivePlayersAsync();

            foreach(Player player in inactivePlayers)
            {
                player.IsLoggedIn = false;
            }

            await _playerRepository.SaveChangesAsync();
        }
    }
}
