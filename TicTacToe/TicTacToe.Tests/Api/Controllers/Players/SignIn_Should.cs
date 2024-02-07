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
    public class SignIn_Should
    {
        private readonly Mock<IPlayerService> _playerServiceMock;

        private readonly PlayerCredentialsDto _playerCredentialsDto;
        private readonly ApiCallDataResponse<int> _expectedResponseValue;

        private readonly string _expectedDatabaseErrorMessage = "An error with the database has occured.";
        private readonly string _expectedFriendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        private readonly string _expectedInvalidSignInCredentialsMessage = "Incorrect Username or Password.";
        private readonly string _expectedPlayerAlreadyLoggedInMessage = "Player is already logged in elsewhere. Please log out of other clients and try again.";

        public SignIn_Should()
        {
            _playerServiceMock = new Mock<IPlayerService>();

            _playerCredentialsDto = new PlayerCredentialsDto();

            _expectedResponseValue = new ApiCallDataResponse<int>
            {
                Dto = 1
            };
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(null);

            // Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Return_Ok_WhenSuccessful()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ReturnsAsync(1);

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equivalent(_expectedResponseValue, result.Value);
        }

        [Fact]
        public async Task Handle_ArgumentNullException_With_BadRequest()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(new ArgumentNullException());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ArgumentException_With_BadRequest()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(new ArgumentException(_expectedInvalidSignInCredentialsMessage));

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedInvalidSignInCredentialsMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_InvalidOperationException_With_BadRequest()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(new InvalidOperationException(_expectedPlayerAlreadyLoggedInMessage));

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(_expectedPlayerAlreadyLoggedInMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_DbUpDateException_With_InternalServerError()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(new DbUpdateException());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

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

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(exception);

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedDatabaseErrorMessage, resultData.ErrorMessage);
        }

        [Fact]
        public async Task Handle_AllOtherExceptions_With_InternalServerError()
        {
            // Arrange

            _playerServiceMock.Setup(psm => psm.SignInAsync(_playerCredentialsDto))
                .ThrowsAsync(new Exception());

            var sut = new PlayersController(_playerServiceMock.Object);

            // Act

            var result = await sut.SignIn(_playerCredentialsDto) as ObjectResult;
            var resultData = result.Value as ApiCallDataResponse<int>;

            // Assert

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(_expectedFriendlyErrorMessage, resultData.ErrorMessage);
        }
    }
}
