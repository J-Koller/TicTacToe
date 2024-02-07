using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TicTacToe.Api.Controllers;
using TicTacToe.Api.Logic.Services.Players;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Api.Shared.Models;
using Xunit;

namespace TicTacToe.Tests.Api.Controllers.Players
{
    public class RegisterNewPlayer_Should
    {
        private readonly Mock<IPlayerService> _playerServiceMock;

        private readonly PlayerCredentialsDto _playerCredentialsDto;
        private readonly ApiCallResponse _expectedResponseValue;

        private const string _expectedUsernameTakenMessage = "Username is already in use.";
        private const string _expectedNotAlphaNumericMessage = "Use only letters or numbers.";
        private readonly string _expectedDatabaseErrorMessage = "An error with the database has occured.";
        private readonly string _expectedFriendlyErrorMessage = "Oops! Something went wrong. Lets try again.";

        public RegisterNewPlayer_Should()
        {
            _playerServiceMock = new Mock<IPlayerService>();

            _playerCredentialsDto = new PlayerCredentialsDto();
            _expectedResponseValue = new ApiCallResponse();
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(null);

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Return_Ok_WhenSuccessful()
        {
            // Arrange

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equivalent(_expectedResponseValue, result.Value);
        }

        [Fact]
        public async Task Handle_ArgumentNullException_With_BadRequest()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(new ArgumentNullException());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
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

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(new ArgumentException(_expectedNotAlphaNumericMessage));

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedNotAlphaNumericMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_InvalidOperationException_With_BadRequest()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(new InvalidOperationException(_expectedUsernameTakenMessage));

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedUsernameTakenMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_DbUpDateException_With_InternalServerError()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(new DbUpdateException());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
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

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(exception);

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
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

            _playerServiceMock.Setup(psm => psm.RegisterNewPlayerAsync(_playerCredentialsDto))
                .ThrowsAsync(new Exception());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.RegisterNewPlayer(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallResponse;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }
    }
}
