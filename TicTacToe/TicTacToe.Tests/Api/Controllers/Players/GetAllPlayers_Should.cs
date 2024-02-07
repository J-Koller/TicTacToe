using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Models;
using Xunit;

namespace TicTacToe.Tests.Api.Controllers.Players
{
    public class GetAllPlayers_Should
    {
        private readonly Mock<IPlayerService> _playerServiceMock;

        private readonly IEnumerable<PlayerDto> _expectedPlayerDtos;
        private readonly ApiCallDataResponse<IEnumerable<PlayerDto>> _expectedResponseValue;

        private readonly string _expectedDatabaseErrorMessage;
        private readonly string _expectedFriendlyErrorMessage;

        public GetAllPlayers_Should()
        {
            _playerServiceMock = new Mock<IPlayerService>();

            _expectedPlayerDtos = new List<PlayerDto>();

            _expectedResponseValue = new ApiCallDataResponse<IEnumerable<PlayerDto>>
            {
                Dto = _expectedPlayerDtos
            };

            _expectedDatabaseErrorMessage = "An error with the database has occured.";
            _expectedFriendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        }


        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.GetAllPlayers();

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Return_Ok_WhenSuccessful()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.GetAllPlayerDtosAsync())
                .ReturnsAsync(_expectedPlayerDtos);

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.GetAllPlayers() as ObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equivalent(_expectedResponseValue, result.Value);
        }

        [Fact]
        public async Task Handle_DbUpDateException_With_InternalServerError()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.GetAllPlayerDtosAsync())
                .ThrowsAsync(new DbUpdateException());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.GetAllPlayers() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<PlayerDto>>;

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

            _playerServiceMock.Setup(psm => psm.GetAllPlayerDtosAsync())
                .ThrowsAsync(exception);

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.GetAllPlayers() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<PlayerDto>>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedDatabaseErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_AllOtherExceptions_With_InternalServerError()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.GetAllPlayerDtosAsync())
                .ThrowsAsync(new Exception());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.GetAllPlayers() as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<IEnumerable<PlayerDto>>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }
    }
}
