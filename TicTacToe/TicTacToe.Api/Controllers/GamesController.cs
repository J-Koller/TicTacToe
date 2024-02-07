using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers.Base;
using TicTacToe.Api.Hubs;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Models;

namespace TicTacToe.Api.Controllers
{
    public class GamesController : CustomControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IHubContext<GameHub> _gameHubContext;

        public GamesController(IGameService gameService, IHubContext<GameHub> hubContext)
        {
            _gameService = gameService;
            _gameHubContext = hubContext;

            _gameService.GameEndAsync += OnGameEndAsync;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<AvailableGameDto>))]
        public async Task<IActionResult> GetAvailableGames()
        {
            var response = new ApiCallDataResponse<IEnumerable<AvailableGameDto>>();

            try
            {
                var availableGames = await _gameService.GetAvailableGamesAsync();
                response.Dto = availableGames;

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(GetAvailableGames), response);
            }
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> StartNewGame([FromBody] NewGameDto newGameDto)
        {
            var response = new ApiCallDataResponse<int>();

            try
            {
                response.Dto = await _gameService.StartNewGameAsync(newGameDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(StartNewGame), response);
            }
        }

        [HttpPost("joingame")]
        public async Task<IActionResult> JoinGame([FromBody] JoinGameDto joinGameDto)
        {
            var response = new ApiCallDataResponse<PlayerDto>();

            try
            {
                response.Dto = await _gameService.JoinGameAsync(joinGameDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(JoinGame), response);
            }
        }

        [HttpPost("endgame")]
        public async Task<IActionResult> EndGame([FromBody] GameResultDto gameResultDto)
        {
            var response = new ApiCallResponse();

            try
            {
                await _gameService.EndGameAsync(gameResultDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(EndGame), response);
            }
        }

        [HttpPost("makemove")]
        public async Task<IActionResult> MakeMove([FromBody] NewMoveDto newMoveDto)
        {
            var response = new ApiCallDataResponse<MoveDto>();

            try
            {
                response.Dto = await _gameService.MakeMoveAsync(newMoveDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(MakeMove), response);
            }
        }

        [HttpPost("abandongame")]
        public async Task<IActionResult> AbandonGame([FromBody] GameResultDto gameResultDto)
        {
            var response = new ApiCallResponse();

            try
            {
                await _gameService.AbandonGameAsync(gameResultDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(AbandonGame), response);
            }
        }

        [HttpPost("clean")]
        public async Task<IActionResult> CleanGames()
        {
            var response = new ApiCallResponse();

            try
            {
                await _gameService.CleanGamesAsync();

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(CleanGames), response);
            }
        }

        private async Task OnGameEndAsync(GameResultDto gameResultDto)
        {
            await _gameHubContext.Clients.Clients(gameResultDto.GameConnectionIds).SendAsync("RecievedGame", gameResultDto);
        }
    }
}
