using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Logic.Services.Symbols;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using TicTacToe.Data.Repositories.Players;

namespace TicTacToe.Api.Logic.Services.Games
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISymbolAssignmentService _symbolAssignmentService;
        private readonly IMoveService _moveService;
        private readonly IMapper _mapper;

        public event Func<GameResultDto, Task> GameEndAsync;

        private readonly string _noWinnerAndNoTieMessage = "Must have a winner if the game is not tied.";
        private readonly string _winnerWithTieMessage = "Cannot have a winner if the game is tied.";
        private readonly string _playerIdMismatchMessage = "Player Id does not match with any players in this game.";
        private readonly string _positionYOutOfBoundsMessage = "Position Y is out of bounds.";
        private readonly string _positionXOutOfBoundsMessage = "Position X is out of bounds.";
        private readonly string _connectionIdNullMessage = "ConnectionId cannot be null or empty.";
        private readonly string _argumentDtoNullMessage = "Argument Dto cannot be null.";
        private readonly string _playerIdLessThanOneMessage = "Player Id cannot be less than one.";
        private readonly string _gameIdLessThanOneMessage = "Game Id cannot be less than one.";

        public GameService(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            ISymbolAssignmentService symbolAssignmentService,
            IMoveService moveService,
            IMapper mapper)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _symbolAssignmentService = symbolAssignmentService;
            _moveService = moveService;
            _mapper = mapper;

            _moveService.MatchingSequenceAsync += EndGameAsync;
        }

        public async Task<IEnumerable<AvailableGameDto>> GetAvailableGamesAsync()
        {
            var availableGames = await _gameRepository.GetGamesLookingForPlayersAsync();

            var availableGameDtos = _mapper.Map<IEnumerable<AvailableGameDto>>(availableGames);

            return availableGameDtos;
        }

        public async Task<int> StartNewGameAsync(NewGameDto newGameDto)
        {
            ArgumentNullException.ThrowIfNull(newGameDto, _argumentDtoNullMessage);

            ArgumentException.ThrowIfNullOrEmpty(newGameDto.GameHubConnectionId, _connectionIdNullMessage);

            if (newGameDto.PlayerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            // validate player is not in the game

            var playerOne = await _playerRepository.GetPlayerByIdAsync(newGameDto.PlayerId);

            var game = new Game()
            {
                StartDate = DateTime.Now,
                IsLookingForPlayers = true,
                IsPlayerOneTurn = true,
                PlayerOneId = newGameDto.PlayerId,
                PlayerOneGameConnectionId = newGameDto.GameHubConnectionId,
            };

            game.Players.Add(playerOne);

            int newGameId = await _gameRepository.AddAsync(game);

            return newGameId;
        }

        public async Task<PlayerDto> JoinGameAsync(JoinGameDto joinGameDto)
        {
            ArgumentNullException.ThrowIfNull(joinGameDto, _argumentDtoNullMessage);
            ArgumentException.ThrowIfNullOrEmpty(joinGameDto.GameHubConnectionId, _connectionIdNullMessage);

            if (joinGameDto.PlayerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            if (joinGameDto.GameId < 1)
            {
                throw new ArgumentException(_gameIdLessThanOneMessage);
            }

            var game = await _gameRepository.GetGameByIdAsync(joinGameDto.GameId);

            bool isAlreadyInTheGame = game.Players.Any(p => p.Id == joinGameDto.PlayerId);

            if (isAlreadyInTheGame)
            {
                throw new InvalidOperationException($"Player:{joinGameDto.PlayerId} is already in the game");
            }

            var playerOne = await _playerRepository.GetPlayerByIdAsync(game.PlayerOneId);
            var playerTwo = await _playerRepository.GetPlayerByIdAsync(joinGameDto.PlayerId);

            game.IsLookingForPlayers = false;
            game.PlayerTwoId = playerTwo.Id;
            game.Players.Add(playerTwo);
            game.PlayerTwoGameConnectionId = joinGameDto.GameHubConnectionId;

            await _gameRepository.SaveChangesAsync();

            var playerOneDto = _mapper.Map<PlayerDto>(playerOne);


            playerOneDto.GameHubConnectionId = game.PlayerOneGameConnectionId;

            return playerOneDto;
        }

        public async Task<MoveDto> MakeMoveAsync(NewMoveDto newMoveDto)
        {
            ArgumentNullException.ThrowIfNull(newMoveDto, _argumentDtoNullMessage);

            if (newMoveDto.PlayerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            if (newMoveDto.GameId < 1)
            {
                throw new ArgumentException(_gameIdLessThanOneMessage);
            }

            if (newMoveDto.PositionX < 0 || newMoveDto.PositionX > 2)
            {
                throw new ArgumentException(_positionXOutOfBoundsMessage);
            }

            if (newMoveDto.PositionY < 0 || newMoveDto.PositionY > 2)
            {
                throw new ArgumentException(_positionYOutOfBoundsMessage);
            }

            var game = await _gameRepository.GetGameByIdAsync(newMoveDto.GameId);

            bool playerIdIsFound = newMoveDto.PlayerId == game.PlayerOneId || newMoveDto.PlayerId == game.PlayerTwoId;

            if (!playerIdIsFound)
            {
                throw new ArgumentException(_playerIdMismatchMessage);
            }

            var move = _mapper.Map<Move>(newMoveDto);

            bool isPlayerOne = newMoveDto.PlayerId == game.PlayerOneId;

            if (isPlayerOne)
            {
                move.Symbol = await _symbolAssignmentService.AssignPlayerOneSymbolAsync();

                game.IsPlayerOneTurn = false;
            }
            else if (!isPlayerOne)
            {
                move.Symbol = await _symbolAssignmentService.AssignPlayerTwoSymbolAsync();

                game.IsPlayerOneTurn = true;
            }

            game.Moves.Add(move);
            await _gameRepository.SaveChangesAsync();

            var allMoveDtos = _mapper.Map<List<MoveDto>>(game.Moves);
            await _moveService.AddMoveAsync(allMoveDtos, game.Id);

            var moveDto = _mapper.Map<MoveDto>(move);
            return moveDto;
        }

        public async Task EndGameAsync(GameResultDto gameResultDto)
        {
            ArgumentNullException.ThrowIfNull(gameResultDto, _argumentDtoNullMessage);

            if (gameResultDto.GameId < 1)
            {
                throw new ArgumentException(_gameIdLessThanOneMessage);
            }

            if (gameResultDto.IsTie && gameResultDto.WinnerId > 0)
            {
                throw new ArgumentException(_winnerWithTieMessage);
            }

            if (!gameResultDto.IsTie && gameResultDto.WinnerId < 1)
            {
                throw new ArgumentException(_noWinnerAndNoTieMessage);
            }

            var game = await _gameRepository.GetGameByIdAsync(gameResultDto.GameId);
            game.FinishDate = DateTime.Now;

            if (!gameResultDto.IsTie)
            {
                game.WinnerId = gameResultDto.WinnerId;
                game.LoserId = game.Players.FirstOrDefault(p => p.Id != game.WinnerId).Id;
            }
            else
            {
                game.IsTied = true;
            }

            await _gameRepository.SaveChangesAsync();

            gameResultDto.GameConnectionIds = new List<string>
            {
                game.PlayerOneGameConnectionId,
                game.PlayerTwoGameConnectionId
            };

            // this is a fix for the null conditional operator not working as intended
            var gameEndAsyncTask = GameEndAsync?.Invoke(gameResultDto) ?? Task.CompletedTask;
            await gameEndAsyncTask;
        }

        public async Task AbandonGameAsync(GameResultDto gameResultDto)
        {
            ArgumentNullException.ThrowIfNull(gameResultDto, _argumentDtoNullMessage);

            if (gameResultDto.GameId < 1)
            {
                throw new ArgumentException(_gameIdLessThanOneMessage);
            }

            var game = await _gameRepository.GetGameByIdAsync(gameResultDto.GameId);

            bool gameHasJustOnePlayer = game.Players.Count == 1;

            if (!gameHasJustOnePlayer && gameResultDto.WinnerId < 1)
            {
                throw new ArgumentException(_playerIdLessThanOneMessage);
            }

            if (gameHasJustOnePlayer)
            {
                game.IsLookingForPlayers = false;
            }
            else
            {
                game.WinnerId = gameResultDto.WinnerId;
                game.LoserId = game.Players.FirstOrDefault(p => p.Id != game.WinnerId).Id;
            }

            game.FinishDate = DateTime.Now;

            await _gameRepository.SaveChangesAsync();
        }

        public async Task CleanGamesAsync()
        {
            var activeGames = await _gameRepository.GetActiveGamesAsync();

            var gamesWithLoggedOutPlayers = activeGames.Where(g => g.Players.Any(p => p.IsLoggedIn == false));

            foreach(Game game in gamesWithLoggedOutPlayers)
            {
                bool isGhostGame = game.Players.Count == 2 && game.Players.All(p => !p.IsLoggedIn);

                if (game.Players.Count == 1 || isGhostGame)
                {
                    game.FinishDate = DateTime.UtcNow;
                    game.IsLookingForPlayers = false;
                }
            }

            await _gameRepository.SaveChangesAsync();

            var fullGamesWithSingleLoggedOutPlayer = gamesWithLoggedOutPlayers.Where(g => g.Players.Where(p => p.IsLoggedIn == true).Count() == 1);

            foreach (Game game in fullGamesWithSingleLoggedOutPlayer)
            {
                game.FinishDate = DateTime.UtcNow;
                game.IsLookingForPlayers = false;
                game.Winner = game.Players.Single(p => p.IsLoggedIn == true);
                game.Loser = game.Players.Single(p => p.IsLoggedIn == false);

                var gameResultDto = new GameResultDto
                {
                    IsTie = false,
                    WinnerId = game.Winner.Id,
                };

                if(game.Winner.Id == game.PlayerOneId)
                {
                    gameResultDto.GameConnectionIds = new() { game.PlayerOneGameConnectionId };
                }
                else
                {
                    gameResultDto.GameConnectionIds = new() { game.PlayerTwoGameConnectionId };
                }

                await _gameRepository.SaveChangesAsync();

                var gameEndAsyncTask = GameEndAsync?.Invoke(gameResultDto) ?? Task.CompletedTask;
                await gameEndAsyncTask;
            }
        }
    }
}
