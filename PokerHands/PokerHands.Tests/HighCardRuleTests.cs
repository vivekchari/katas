using System;
using Xunit;
using FluentAssertions;

namespace PokerHands.Tests
{
    public class HighCardRuleTests
    {
        private ICardRule _highCardRule;
        private ICardRule _pairCardRule;

        public HighCardRuleTests()
        {
            _highCardRule = new HighCardRule();
            _pairCardRule = new PairOfTwoCardRule();
        }

        [Fact]
        public void WithTwoHandsHighCardWins()
        {
            var player1Cards = new Player("player1", "2H AC".ToCards());
            var player2Cards = new Player("player2", "3H 3C".ToCards());

            var winner = _highCardRule.FindWinner(player1Cards, player2Cards);

            winner.Name.Should().Be("player1");

        }

        [Fact]
        public void WithTwoHandsSameHighCardConsidersNextHighCard()
        {
            var player1Cards = new Player("player1", "2H AC".ToCards());
            var player2Cards = new Player("player2", "3H AH".ToCards());

            var winner = _highCardRule.FindWinner(player1Cards, player2Cards);

            winner.Name.Should().Be("player2");
        }

    }
}