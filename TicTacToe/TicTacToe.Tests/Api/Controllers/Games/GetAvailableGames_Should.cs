using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Models;
using Xunit;

namespace TicTacToe.Tests.Api.Controllers.Games
{
    public class GetAvailableGames_Should
    {
        private readonly Mock<IGameService> _gameServiceMock;

        private readonly IEnumerable<AvailableGameDto> _expectedAvailableGames;
        private readonly ApiCallDataResponse<IEnumerable<AvailableGameDto>> _expectedResponseValue;

        private readonly string _expectedDatabaseErrorMessage;
        private readonly string _expectedFriendlyErrorMessage;

        public GetAvailableGames_Should()
        {
            _gameServiceMock = new Mock<IGameService>();

            _expectedAvailableGames = new List<AvailableGameDto>
            {
                new AvailableGameDto()
            };

            _expectedResponseValue = new ApiCallDataResponse<IEnumerable<AvailableGameDto>>
            {
                Dto = _expectedAvailableGames
            };

            _expectedDatabaseErrorMessage = "An error with the database has occured.";
            _expectedFriendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.GetAvailableGamesAsync())
                .ReturnsAsync(_expectedAvailableGames);
          
            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.GetAvailableGames();

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Return_Ok_WhenSuccessful()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.GetAvailableGamesAsync())
                .ReturnsAsync(_expectedAvailableGames);

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.GetAvailableGames() as ObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equivalent(_expectedResponseValue, result.Value);
        }


        [Fact]
        public async Task Handle_DbUpDateException_With_InternalServerError()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.GetAvailableGamesAsync())
                .ThrowsAsync(new DbUpdateException());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.GetAvailableGames() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<AvailableGameDto>>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedDatabaseErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_SqlException_With_InternalServerError()
        {
            // Arrange

            var exception = FormatterServices.GetUninitializedObject(typeof(SqlException))
             as SqlException;

            _gameServiceMock.Setup(gsm => gsm.GetAvailableGamesAsync())
                .ThrowsAsync(exception);

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.GetAvailableGames() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<AvailableGameDto>>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedDatabaseErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_AllOtherExceptions_With_InternalServerError()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.GetAvailableGamesAsync())
                .ThrowsAsync(new Exception());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.GetAvailableGames() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<AvailableGameDto>>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }
    }
}
