using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers.Base;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Models;

namespace TicTacToe.Api.Controllers
{
    public class PlayersController : CustomControllerBase
    {
        private readonly IPlayerService _playerService;

        private readonly string _databaseErrorMessage = "An error with the database has occured.";
        private readonly string _friendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        private readonly string _errorLogTemplate = "Error at: {@EndpointName}... ErrorMessage: {@ErrorMessage}";

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<PlayerDto>))]
        public async Task<IActionResult> GetAllPlayers()
        {
            var response = new ApiCallDataResponse<IEnumerable<PlayerDto>>();

            try
            {
                var allPlayerDtos = await _playerService.GetAllPlayerDtosAsync();
                response.Dto = allPlayerDtos;

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(GetAllPlayers), response);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] PlayerCredentialsDto playerCredentialsDto)
        {
            var response = new ApiCallDataResponse<int>();

            try
            {
                int playerId = await _playerService.SignInAsync(playerCredentialsDto);
                response.Dto = playerId;

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleUniqueExceptions(e, nameof(SignIn), response);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewPlayer([FromBody] PlayerCredentialsDto playerCredentialsDto)
        {
            var response = new ApiCallResponse();

            try
            {
                await _playerService.RegisterNewPlayerAsync(playerCredentialsDto);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleUniqueExceptions(e, nameof(RegisterNewPlayer), response);
            }
        }

        [HttpGet("getplayer/{playerId}")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            var response = new ApiCallDataResponse<PlayerDto>();

            try
            {
                var playerDto = await _playerService.GetPlayerDtoAsync(playerId);
                response.Dto = playerDto;
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return HandleStandardExceptions(e, nameof(GetPlayer), response);
            }
        }

        [HttpPost("signout/{playerId}")]
        public async Task<IActionResult> SignOut(int playerId)
        {
            var response = new ApiCallResponse();

            try
            {
                await _playerService.SignOutAsync(playerId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(SignOut), response);
            }
        }

        [HttpPost("clean")]
        public async Task<IActionResult> CleanInactivePlayers()
        {
            var response = new ApiCallResponse();

            try
            {
                await _playerService.CleanInactivePlayersAsync();

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(CleanInactivePlayers), response);
            }
        }

        [HttpPost("heartbeat/{playerId}")]
        public async Task<IActionResult> HeartBeat(int playerId)
        {
            var response = new ApiCallResponse();

            try
            {
                await _playerService.PlayerHeartBeatAsync(playerId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleStandardExceptions(e, nameof(HeartBeat), response);
            }
        }
    }
}