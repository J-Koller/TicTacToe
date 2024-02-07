using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Api.Logic.Services.Games;
using TicTacToe.Api.Logic.Services.Moves;
using TicTacToe.Api.Logic.Services.Symbols;
using TicTacToe.Api.Shared.Dto;
using TicTacToe.Data.Entities;
using TicTacToe.Data.Repositories.Games;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Games
{
    public class MakeMoveAsync_Should
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<ISymbolAssignmentService> _symbolAssignmentServiceMock;
        private readonly Mock<IMoveService> _moveServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private NewMoveDto _newMoveDto;
        private Game _game;
        private Move _move;
        private Symbol _symbol;
        private List<MoveDto> _allMoveDtos;
        private MoveDto _moveDto;

        public MakeMoveAsync_Should()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _symbolAssignmentServiceMock = new Mock<ISymbolAssignmentService>();
            _moveServiceMock = new Mock<IMoveService>();
            _mapperMock = new Mock<IMapper>();

            _newMoveDto = new NewMoveDto
            {
                PlayerId = 1,
                GameId = 1,
                PositionX = 1,
                PositionY = 1
            };

            _game = new Game
            {
                Id = 1,
                PlayerOneId = 1,
                PlayerTwoId = 2
            };

            _move = new Move
            {
                PlayerId = 1,
                GameId = 1,
                PositionX = 1,
                PositionY = 1
            };

            _symbol = new Symbol
            {
                Value = "X"
            };

            _allMoveDtos = new List<MoveDto>();

            _moveDto = new MoveDto();

            _game.Moves.Add(_move);
        }

        [Fact]
        public async Task ThrowIf_Argument_IsNull()
        {
            // Arrange

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(null);

            // Assert

            await Assert.ThrowsAsync<ArgumentNullException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_PlayerId_IsLessThanOne()
        {
            // Arrange

            _newMoveDto.PlayerId = 0;

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(_newMoveDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_GameId_IsLessThanOne()
        {
            // Arrange

            _newMoveDto.GameId = 0;

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(_newMoveDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public async Task ThrowIf_Argument_PositionX_IsOutOfBounds(int positionX)
        {
            // Arrange

            _newMoveDto.PositionX = positionX;

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(_newMoveDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public async Task ThrowIf_Argument_PositionY_IsOutOfBounds(int positionY)
        {
            // Arrange

            _newMoveDto.PositionY = positionY;

            var sut = new GameService(null, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(_newMoveDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task ThrowIf_Argument_PlayerId_IsNotFoundInGame()
        {
            // Arrange

            _newMoveDto.PlayerId = 3;

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);


            var sut = new GameService(_gameRepositoryMock.Object, null, null, _moveServiceMock.Object, null);

            // Act

            var testingCode = () => sut.MakeMoveAsync(_newMoveDto);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(testingCode);
        }

        [Fact]
        public async Task AssignValueTo_Move_Symbol_AsPlayerOne()
        {
            // Arrange

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _symbolAssignmentServiceMock.Setup(sasm => sasm.AssignPlayerOneSymbolAsync()).ReturnsAsync(_symbol);
            _mapperMock.Setup(mm => mm.Map<Move>(_newMoveDto)).Returns(_move);
            _mapperMock.Setup(mm => mm.Map<List<MoveDto>>(_game.Moves)).Returns(_allMoveDtos);
            _mapperMock.Setup(mm => mm.Map<MoveDto>(_move)).Returns(_moveDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                _symbolAssignmentServiceMock.Object,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var moveDto = await sut.MakeMoveAsync(_newMoveDto);

            // Assert

            _symbolAssignmentServiceMock.Verify(sasm => sasm.AssignPlayerOneSymbolAsync());
        }

        [Fact]
        public async Task AssignValueTo_Move_Symbol_AsPlayerTwo()
        {
            // Arrange

            _newMoveDto.PlayerId = 2;

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _symbolAssignmentServiceMock.Setup(sasm => sasm.AssignPlayerTwoSymbolAsync()).ReturnsAsync(_symbol);
            _mapperMock.Setup(mm => mm.Map<Move>(_newMoveDto)).Returns(_move);
            _mapperMock.Setup(mm => mm.Map<List<MoveDto>>(_game.Moves)).Returns(_allMoveDtos);
            _mapperMock.Setup(mm => mm.Map<MoveDto>(_move)).Returns(_moveDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                _symbolAssignmentServiceMock.Object,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var moveDto = await sut.MakeMoveAsync(_newMoveDto);

            // Assert

            _symbolAssignmentServiceMock.Verify(sasm => sasm.AssignPlayerTwoSymbolAsync());
        }

        [Fact]
        public async Task Call_SaveChanges()
        {
            // Arrange

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _symbolAssignmentServiceMock.Setup(sasm => sasm.AssignPlayerOneSymbolAsync()).ReturnsAsync(_symbol);
            _mapperMock.Setup(mm => mm.Map<Move>(_newMoveDto)).Returns(_move);
            _mapperMock.Setup(mm => mm.Map<List<MoveDto>>(_game.Moves)).Returns(_allMoveDtos);
            _mapperMock.Setup(mm => mm.Map<MoveDto>(_move)).Returns(_moveDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                _symbolAssignmentServiceMock.Object,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var moveDto = await sut.MakeMoveAsync(_newMoveDto);

            // Assert

            _gameRepositoryMock.Verify(grm => grm.SaveChangesAsync());
        }

        [Fact]
        public async Task AddMoveTo_MoveService()
        {
            // Arrange

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _symbolAssignmentServiceMock.Setup(sasm => sasm.AssignPlayerOneSymbolAsync()).ReturnsAsync(_symbol);
            _mapperMock.Setup(mm => mm.Map<Move>(_newMoveDto)).Returns(_move);
            _mapperMock.Setup(mm => mm.Map<List<MoveDto>>(_game.Moves)).Returns(_allMoveDtos);
            _mapperMock.Setup(mm => mm.Map<MoveDto>(_move)).Returns(_moveDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                _symbolAssignmentServiceMock.Object,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var moveDto = await sut.MakeMoveAsync(_newMoveDto);

            // Assert

            _moveServiceMock.Verify(msm => msm.AddMoveAsync(_allMoveDtos, _game.Id));
        }

        [Fact]
        public async Task NotReturn_Null()
        {
            // Arrange

            _gameRepositoryMock.Setup(grm => grm.GetGameByIdAsync(1)).ReturnsAsync(_game);
            _symbolAssignmentServiceMock.Setup(sasm => sasm.AssignPlayerOneSymbolAsync()).ReturnsAsync(_symbol);
            _mapperMock.Setup(mm => mm.Map<Move>(_newMoveDto)).Returns(_move);
            _mapperMock.Setup(mm => mm.Map<List<MoveDto>>(_game.Moves)).Returns(_allMoveDtos);
            _mapperMock.Setup(mm => mm.Map<MoveDto>(_move)).Returns(_moveDto);

            var sut = new GameService(
                _gameRepositoryMock.Object,
                null,
                _symbolAssignmentServiceMock.Object,
                _moveServiceMock.Object,
                _mapperMock.Object);

            // Act

            var moveDto = await sut.MakeMoveAsync(_newMoveDto);

            // Assert

            Assert.NotNull(moveDto);
        }
    }
}
