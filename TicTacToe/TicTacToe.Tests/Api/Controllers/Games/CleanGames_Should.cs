﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Shared.Models;
using Xunit;

namespace TicTacToe.Tests.Api.Controllers.Games
{
    public class CleanGames_Should
    {
        private readonly Mock<IGameService> _gameServiceMock;

        private readonly ApiCallResponse _expectedResponseValue;

        private readonly string _expectedDatabaseErrorMessage;
        private readonly string _expectedFriendlyErrorMessage;
        public CleanGames_Should()
        {
            _gameServiceMock = new Mock<IGameService>();

            _expectedResponseValue = new ApiCallResponse();

            _expectedDatabaseErrorMessage = "An error with the database has occured.";
            _expectedFriendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames();

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Return_Ok_WhenSuccessful()
        {
            // Arrange

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equivalent(_expectedResponseValue, result.Value);
        }

        [Fact]
        public async Task Handle_ArgumentNullException_With_BadRequest()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.CleanGamesAsync())
                .ThrowsAsync(new ArgumentNullException());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ArgumentException_With_BadRequest()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.CleanGamesAsync())
                .ThrowsAsync(new ArgumentException());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_DbUpDateException_With_InternalServerError()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.CleanGamesAsync())
                .ThrowsAsync(new DbUpdateException());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

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

            _gameServiceMock.Setup(gsm => gsm.CleanGamesAsync())
                .ThrowsAsync(exception);

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedDatabaseErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_AllOtherExceptions_With_InternalServerError()
        {
            // Arrange

            _gameServiceMock.Setup(gsm => gsm.CleanGamesAsync())
                .ThrowsAsync(new Exception());

            var sut = new GamesController(_gameServiceMock.Object, null);

            // Act

            var result = await sut.CleanGames() as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }
    }
}