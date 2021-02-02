
using System;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace PokerHands.Tests
{
    public class PlayerWithHigestCardTests
    {

        [Theory]
        [InlineData("2H AH", "2H AH", null)]
        [InlineData("3H 3C 3D", "AH AC AD", "player2")]
        [InlineData("2H 2D", "2H 2C", null)]
        [InlineData("2H 3D AH", "KC 2C JD", "player1")]
        public void PlayerWithHighestCard(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player> {
            new Player("player1", player1Cards.ToCards()),
            new Player("player2", player2Cards.ToCards())
            };

            var winner = players.SelectPlayerWithHighCard();

            if (expectedWinner != null)
            {
                winner.Should().NotBeNull();
                winner.Name.Should().Be(expectedWinner);
            }
            else
            {
                winner.Should().BeNull();
            }
        }
    }
}