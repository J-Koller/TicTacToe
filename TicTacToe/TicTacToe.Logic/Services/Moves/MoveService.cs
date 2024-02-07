using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Dto;

namespace TicTacToe.Api.Logic.Services.Moves
{
    public class MoveService : IMoveService
    {
        private List<MoveDto> _diagFromTopLeft { get; set; }
        private List<MoveDto> _diagFromTopRight { get; set; }
        private List<MoveDto> _topRow { get; set; }
        private List<MoveDto> _middleRow { get; set; }
        private List<MoveDto> _bottomRow { get; set; }
        private List<MoveDto> _leftColumn { get; set; }
        private List<MoveDto> _middleColumn { get; set; }
        private List<MoveDto> _rightColumn { get; set; }

        public event Func<GameResultDto, Task> MatchingSequenceAsync;

        public MoveService()
        {
            _diagFromTopLeft = new List<MoveDto>();
            _diagFromTopRight = new List<MoveDto>();
            _topRow = new List<MoveDto>();
            _middleRow = new List<MoveDto>();
            _bottomRow = new List<MoveDto>();
            _leftColumn = new List<MoveDto>();
            _middleColumn = new List<MoveDto>();
            _rightColumn = new List<MoveDto>();
        }

        public async Task AddMoveAsync(List<MoveDto> moveDtos, int gameId)
        {
            ArgumentNullException.ThrowIfNull(moveDtos);

            if (moveDtos.Any(m => m == null))
            {
                throw new ArgumentNullException("MoveDtos contains a null.");
            }

            if (gameId < 1)
            {
                throw new ArgumentException("Game Id cannot be less than one.");
            }

            foreach (MoveDto moveDto in moveDtos)
            {
                CategorizeMovesByCoordinates(moveDto);
            }

            await CheckForMatchingSequence(gameId);
        }

        private void CategorizeMovesByCoordinates(MoveDto moveDto)
        {
            if (moveDto.Coordinates.X == 0 && moveDto.Coordinates.Y >= 0)
            {
                _topRow.Add(moveDto);

                if (moveDto.Coordinates.Y == 0)
                {
                    _diagFromTopLeft.Add(moveDto);
                }

                if (moveDto.Coordinates.Y == 2)
                {
                    _diagFromTopRight.Add(moveDto);
                }
            }

            if (moveDto.Coordinates.X == 1 && moveDto.Coordinates.Y >= 0)
            {
                _middleRow.Add(moveDto);

                if (moveDto.Coordinates.Y == 1)
                {
                    _diagFromTopLeft.Add(moveDto);
                    _diagFromTopRight.Add(moveDto);
                }
            }

            if (moveDto.Coordinates.X == 2 && moveDto.Coordinates.Y >= 0)
            {
                _bottomRow.Add(moveDto);

                if (moveDto.Coordinates.Y == 0)
                {
                    _diagFromTopRight.Add(moveDto);
                }

                if (moveDto.Coordinates.Y == 2)
                {
                    _diagFromTopLeft.Add(moveDto);
                }
            }

            if (moveDto.Coordinates.Y == 0 && moveDto.Coordinates.X >= 0)
            {
                _leftColumn.Add(moveDto);
            }

            if (moveDto.Coordinates.Y == 1 && moveDto.Coordinates.X >= 0)
            {
                _middleColumn.Add(moveDto);
            }

            if (moveDto.Coordinates.Y == 2 && moveDto.Coordinates.X >= 0)
            {
                _rightColumn.Add(moveDto);
            }
        }
        private async Task CheckForMatchingSequence(int gameId)
        {
            var gameResultDto = new GameResultDto
            {
                GameId = gameId
            };

            bool topRowMatch = _topRow.Count == 3 && _topRow.All(m => m.PlayerId == _topRow[0].PlayerId);
            bool middleRowMatch = _middleRow.Count == 3 && _middleRow.All(m => m.PlayerId == _middleRow[0].PlayerId);
            bool bottomRowMatch = _bottomRow.Count == 3 && _bottomRow.All(m => m.PlayerId == _bottomRow[0].PlayerId);

            bool leftColumnMatch = _leftColumn.Count == 3 && _leftColumn.All(m => m.PlayerId == _leftColumn[0].PlayerId);
            bool middleColumnMatch = _middleColumn.Count == 3 && _middleColumn.All(m => m.PlayerId == _middleColumn[0].PlayerId);
            bool rightColumnMatch = _rightColumn.Count == 3 && _rightColumn.All(m => m.PlayerId == _rightColumn[0].PlayerId);

            bool diagFromTopLeftMatch = _diagFromTopLeft.Count == 3 && _diagFromTopLeft.All(m => m.PlayerId == _diagFromTopLeft[0].PlayerId);
            bool diagFromTopRightMatch = _diagFromTopRight.Count == 3 && _diagFromTopRight.All(m => m.PlayerId == _diagFromTopRight[0].PlayerId);

            List<bool> potentialMatches = new List<bool>()
            {
                topRowMatch,
                middleRowMatch,
                bottomRowMatch,
                leftColumnMatch,
                middleColumnMatch,
                rightColumnMatch,
                diagFromTopLeftMatch,
                diagFromTopRightMatch
            };

            bool matchExists = potentialMatches.Any(b => b == true);

            bool allPositionsFilled = _topRow.Count == 3 &&
                                      _middleRow.Count == 3 &&
                                      _bottomRow.Count == 3;
            if (!matchExists)
            {
                if (allPositionsFilled)
                {
                    gameResultDto.IsTie = true;

                    await MatchingSequenceAsync?.Invoke(gameResultDto);
                }

                return;
            }

            if (topRowMatch)
            {
                gameResultDto.WinnerId = _topRow[0].PlayerId;
            }

            if (middleRowMatch)
            {
                gameResultDto.WinnerId = _middleRow[0].PlayerId;
            }

            if (bottomRowMatch)
            {
                gameResultDto.WinnerId = _bottomRow[0].PlayerId;
            }

            if (leftColumnMatch)
            {
                gameResultDto.WinnerId = _leftColumn[0].PlayerId;
            }

            if (middleColumnMatch)
            {
                gameResultDto.WinnerId = _middleColumn[0].PlayerId;
            }

            if (rightColumnMatch)
            {
                gameResultDto.WinnerId = _rightColumn[0].PlayerId;
            }

            if (diagFromTopLeftMatch)
            {
                gameResultDto.WinnerId = _diagFromTopLeft[0].PlayerId;
            }

            if (diagFromTopRightMatch)
            {
                gameResultDto.WinnerId = _diagFromTopRight[0].PlayerId;
            }

            await MatchingSequenceAsync?.Invoke(gameResultDto);
        }
    }
}
