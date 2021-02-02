using System.Collections.Generic;
using System.Linq;
using static PlayerExtensions;

public static class PokerHandRules
{
    public static (Player Winner, PokerHand WinningHand) WithHighCard(PokerGame game)
    {
        var playerWithHighestCard = game
                    .Players
                    .SelectPlayersWithUniqueCards()
                    .SelectPlayerWithHighCard();

        return (playerWithHighestCard, PokerHand.HighCard);
    }

    public static (Player Winner, PokerHand WinningHand) WithAPair(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(2).SelectPlayerWithHighCard(), PokerHand.TwoOfAKind);

    public static (Player Winner, PokerHand WinningHand) WithTwoPairs(PokerGame game) =>
        (game.Players.Where(HasTwoPairs).SelectPlayersWithSameCards(2).SelectPlayerWithHighCard(), PokerHand.TwoPair);

    public static (Player Winner, PokerHand WinningHand) WithThreeOfAKind(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(3).SelectPlayerWithHighCard(), PokerHand.ThreeOfAKind);

    public static (Player Winner, PokerHand WinningHand) WithAFlush(PokerGame game) =>
        (game.Players.Where(HasFlush).SelectPlayerWithHighCard(), PokerHand.Flush);

    public static (Player Winner, PokerHand WinningHand) WithAFullHouse(PokerGame game) =>
        (game.Players.Where(HasFullHouse).SelectPlayerWithHighCard(), PokerHand.FullHouse);

    public static (Player Winner, PokerHand WinningHand) WithFourOfAKind(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(4).SelectPlayerWithHighCard(), PokerHand.FourOfAKind);
    public static (Player Winner, PokerHand WinningHand) WithStraight(PokerGame game) =>
        (game.Players.Where(HasConsecutive).SelectPlayerWithHighCard(), PokerHand.StraightFlush);

    public static (Player Winner, PokerHand WinningHand) WithStraightFlush(PokerGame game) =>
        (game.Players.Where(HasStraightFlush).SelectPlayerWithHighCard(), PokerHand.StraightFlush);
}