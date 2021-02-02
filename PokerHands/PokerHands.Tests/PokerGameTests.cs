using Xunit;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;

using static PokerHandRules;
using static PlayerExtensions;

namespace PokerHands.Tests
{
    public class PokerGameTests
    {
        [Fact]
        public void HighCard_OnePlayerWitHighestCard()
        {
            var players = new List<Player>
            {
                new Player("player1", "2H AC".ToCards()),
                new Player("player2", "3S 4C".ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithHighestCard);

            game.Winner.Should().NotBeNull();
            game.WinningHand.Should().Be(PokerHand.HighCard);
            game.Winner.Name.Should().Be("player1");
        }

        [Theory]
        [InlineData("2H AC 5H", "3S 4C 7S", "player1")]
        [InlineData("2H AC 5H", "3S AD 7S", "player2")]
        public void HighCard_MultiplePlayersWithSameHighCard(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithHighestCard);

            game.Winner.Should().NotBeNull();
            game.WinningHand.Should().Be(PokerHand.HighCard);
            game.Winner.Name.Should().Be(expectedWinner);
        }

        [Fact]
        public void HighCard_SameHands_CantDetermineWinner()
        {
            var players = new List<Player>
            {
                new Player("player1", "2H AC KH".ToCards()),
                new Player("player2", "2S AD KH".ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithHighestCard);

            game.Winner.Should().BeNull();
        }

        [Theory]
        [InlineData("2H 2C 5H", "3S 4C 7S", "player1")]
        [InlineData("2H 5C 7H", "3S 7C 7S", "player2")]
        [InlineData("2H 5C 5H", "3S 2C 2S", "player1")]
        public void PokerHand_WithPair(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithAPair);

            game.Winner.Should().NotBeNull();
            game.Winner.Name.Should().Be(expectedWinner);
        }

        [Fact]
        public void TwoOfAKind_WithoutAnyPair_CantDetermineWinner()
        {
            var players = new List<Player>
            {
                new Player("player1", "2H 4C AH".ToCards()),
                new Player("player2", "2S 7H AS".ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithAPair);

            game.Winner.Should().BeNull();
        }

        [Fact]
        public void TwoOfAKind_WithoutSamePair_CantDetermineWinner()
        {
            var players = new List<Player>
            {
                new Player("player1", "2H AC AH".ToCards()),
                new Player("player2", "5S AD AS".ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithAPair);

            game.Winner.Should().BeNull();
        }

        [Theory]
        [InlineData("2H 2C 5H 6H KD", "4S 4C 7S 7D AH", "player2")]
        [InlineData("2H 2C 5H 5C KD", "4S 4C 7S 7D AH", "player2")]
        [InlineData("2H 2C 5H 5C KD", "4S 4C 3S 3D AH", "player1")]
        [InlineData("2H 4D 4H AH KD", "6S 4D 4S 3H 3D", "player2")]
        //[InlineData("AH 4D 4H AH KD", "QS AD AS KH KD", "player2")]
        [InlineData("AH 4D 4H AH KD", "QS 4D 4S AH AD", null)]
        public void PokerHand_WithTwoPair(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithTwoPairs);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("2H 2C 2H 6H KD", "4S 4C 7S 7D AH", "player1")]
        [InlineData("2H 2C 2H 6H KD", "4S 7C 7S 7D AH", "player2")]
        [InlineData("2H 2C AS AC AD", "4S 7C AS AD AH", null)]
        [InlineData("2H 2C AD", "4S 7C AH", null)]
        public void PokerHand_WithThreeOfAKind(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithThreeOfAKind);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("2H 4H 3H 6H KH", "4S 4C 7S 7D AH", "player1")]
        [InlineData("2H 4H 3H 6H KS", "4S 4S 7S 7S AS", "player2")]
        [InlineData("2H 4H 3H 6H AH", "4S 4S 7S 7S JS", "player1")]
        public void PokerHand_WithFlush(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithAFlush);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("2H 2C 3H 3S 3D", "4S 4C 7S 7D AH", "player1")]
        [InlineData("2H 2C 3H 3S 3D", "4S 4C 7S 7D 7H", "player2")]
        public void PokerHand_WithFullHouse(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithAFullHouse);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("2H 4H 3H 6H KH", "4S 4C 4S 4D AH", "player2")]
        [InlineData("KH KD KC KS 2D", "4S 4C 4S 4D AH", "player1")]
        public void PokerHand_WithFourOfAKind(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithFourOfAKind);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("TS JS QS KS AS")]
        [InlineData("2S 5D 4C 3S 6H")]
        public void IsConsecutiveTest(string cards)
        {
            var player = new Player("player1", cards.ToCards());
            HasConsecutive(player).Should().BeTrue();
        }

        [Theory]
        [InlineData("2H 3H 4H 5H 6H", "4S 4C 4S 4D AH", "player1")]
        [InlineData("2H 3H 4H 5H 6H", "TS JS QS KS AS", "player2")]
        public void PokerHand_WithStraightFlush(string player1Cards, string player2Cards, string expectedWinner)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithStraightFlush);

            if (expectedWinner == null)
            {
                game.Winner.Should().BeNull();
            }
            else
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
            }
        }

        [Theory]
        [InlineData("2H 2C 5H", "3S 4C 7S", "player1", PokerHand.TwoOfAKind)]
        [InlineData("2H AC 5H", "3S 4C 7S", "player1", PokerHand.HighCard)]
        [InlineData("2H 4D 4H", "6S 4D 4S", "player2", PokerHand.HighCard)]
        [InlineData("2H 4D AC AD AH", "6S 5S 4S AS 2S", "player2", PokerHand.Flush)]
        [InlineData("4H 4D AC AD AH", "6S 5S 4S AS 2S", "player1", PokerHand.FullHouse)]
        // [InlineData("2H 4D 4H AH KD", "6S 4D 4S 3H 3D", "player2", PokerHand.TwoPair)]
        [InlineData("2H 4D 4H", "2S 4C 4S", null, null)]
        public void PokerGame_Rules(string player1Cards, string player2Cards, string expectedWinner, PokerHand? expectedWinningHand)
        {
            var players = new List<Player>
            {
                new Player("player1", player1Cards.ToCards()),
                new Player("player2", player2Cards.ToCards())
            };
            var game = new PokerGame(players)
                .FindWinner(PlayerWithStraightFlush)
                .FindWinner(PlayerWithFourOfAKind)
                .FindWinner(PlayerWithAFullHouse)
                .FindWinner(PlayerWithAFlush)
                .FindWinner(PlayerWithStraight)
                .FindWinner(PlayerWithThreeOfAKind)
                .FindWinner(PlayerWithTwoPairs)
                .FindWinner(PlayerWithAPair)
                .FindWinner(PlayerWithHighestCard);

            if (expectedWinner != null)
            {
                game.Winner.Should().NotBeNull();
                game.Winner.Name.Should().Be(expectedWinner);
                game.WinningHand.Should().Be(expectedWinningHand);
            }
            else
            {
                game.Winner.Should().BeNull();
            }
        }
    }
}