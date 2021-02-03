using System.Collections.Generic;
using System.Linq;
using static PlayerExtensions;

public static class PokerHandRules
{
    public static (Player Winner, PokerHand WinningHand) WithHighCard(PokerGame game) =>
         (game.Players.SelectPlayersWithUniqueCards().SelectPlayerWithHighCard(), PokerHand.HighCard);

    public static (Player Winner, PokerHand WinningHand) WithAPair(PokerGame game) =>
        (game.Players.Where(HasAPair).Select(PlayersWithAPair).SelectPlayerWithHighCard(), PokerHand.TwoOfAKind);

    public static (Player Winner, PokerHand WinningHand) WithTwoPairs(PokerGame game) =>
        (game.Players.Where(HasTwoPairs).Select(PlayersWithAPair).SelectPlayerWithHighCard(), PokerHand.TwoPair);

    public static (Player Winner, PokerHand WinningHand) WithThreeOfAKind(PokerGame game) =>
        (game.Players.Where(HasThreeOfAKind).Select(PlayersWithThreeOfAKind).SelectPlayerWithHighCard(), PokerHand.ThreeOfAKind);

    public static (Player Winner, PokerHand WinningHand) WithStraight(PokerGame game) =>
        (game.Players.Where(HasConsecutive).SelectPlayerWithHighCard(), PokerHand.StraightFlush);
    public static (Player Winner, PokerHand WinningHand) WithAFlush(PokerGame game) =>
        (game.Players.Where(HasFlush).SelectPlayerWithHighCard(), PokerHand.Flush);

    public static (Player Winner, PokerHand WinningHand) WithAFullHouse(PokerGame game) =>
        (game.Players.Where(HasFullHouse).SelectPlayerWithHighCard(), PokerHand.FullHouse);

    public static (Player Winner, PokerHand WinningHand) WithFourOfAKind(PokerGame game) =>
        (game.Players.Where(HasFourOfAKind).Select(PlayersWithFourOfAKind).SelectPlayerWithHighCard(), PokerHand.FourOfAKind);

    public static (Player Winner, PokerHand WinningHand) WithStraightFlush(PokerGame game) =>
        (game.Players.Where(HasStraightFlush).SelectPlayerWithHighCard(), PokerHand.StraightFlush);
}