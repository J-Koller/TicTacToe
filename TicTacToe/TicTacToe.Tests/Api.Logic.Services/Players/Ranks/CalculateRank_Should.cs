using System;
using TicTacToe.Api.Logic.Services.Players.Ranks;
using Xunit;

namespace TicTacToe.Tests.Api.Logic.Services.Players.Ranks
{
    public class CalculateRank_Should
    {
        private readonly Random _random = new Random();

        [Fact]
        public void NotReturn_Null()
        {
            // Arrange 

            int randomWins = _random.Next();
            int randomLosses = _random.Next();

            var sut = new RankService();

            // Act

            string newRank = sut.CalculateRank(randomWins, randomLosses);

            // Assert

            Assert.NotNull(newRank);
        }

        [Theory]
        [InlineData(3, -1)]
        [InlineData(-1, 3)]
        public void ThrowIf_IntArguments_AreNegative(int gamesWon, int gamesLost)
        {
            // Arrange

            var sut = new RankService();

            // Act

            var testingCode = () => sut.CalculateRank(gamesWon, gamesLost);

            // Assert

            Assert.Throws<ArgumentException>(testingCode);
        }
    }
}
