using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace PokerHands.Tests
{
    public class CardInputTests
    {
        [Fact]
        public void HandIsParsedCorrectly()
        {
            var input = "2C 2H";
            var cards = input.ToCards();
            cards.Should().NotBeNull();
            cards.Count().Should().Be(2);
        }

        [Theory]
        [InlineData("2C", Suite.Club)]
        [InlineData("2H", Suite.Hearts)]
        [InlineData("2D", Suite.Diamond)]
        [InlineData("2S", Suite.Spade)]
        public void CardSuiteIsParsedCorrectly(string input, Suite expectedSuite)
        {
            var cards = input.ToCards();
            cards.First().suite.Should().Be(expectedSuite);
        }


        [Theory]
        [InlineData("2C", 2)]
        [InlineData("JH", 11)]
        [InlineData("QD", 12)]
        [InlineData("KS", 13)]
        [InlineData("AS", 14)]
        public void CardValueIsParsedCorrectly(string input, int expectedValue)
        {
            var cards = input.ToCards();
            cards.First().value.Should().Be(expectedValue);
        }

        [Fact]
        public void CardWithValueAndSuiteIsParsedCorrectly()
        {
            var cards = "2C KS".ToCards();
            cards.Count().Should().Be(2);
        }
    }
}
