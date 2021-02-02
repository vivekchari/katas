using System.Collections.Generic;
using System.Linq;
using static PlayerExtensions;

public static class PokerHandRules
{
    public static (Player Winner, PokerHand WinningHand) PlayerWithHighestCard(PokerGame game)
    {
        var playerWithHighestCard = game
                    .Players
                    .Select(p =>
                            new Player(
                                    p.Name,
                                    p.Hand.Except(game.Players.Except(new List<Player> { p }).SelectMany(p => p.Hand))
                            )).FindPlayerWithHighestCard();

        return (playerWithHighestCard, PokerHand.HighCard);
    }

    public static (Player Winner, PokerHand WinningHand) PlayerWithAPair(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(2).FindPlayerWithHighestCard(), PokerHand.TwoOfAKind);

    public static (Player Winner, PokerHand WinningHand) PlayerWithTwoPairs(PokerGame game) =>
        (game.Players.Where(HasAPairOfTwoCards).SelectPlayersWithSameCards(2).FindPlayerWithHighestCard(), PokerHand.TwoPair);

    public static (Player Winner, PokerHand WinningHand) PlayerWithThreeOfAKind(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(3).FindPlayerWithHighestCard(), PokerHand.ThreeOfAKind);

    public static (Player Winner, PokerHand WinningHand) PlayerWithAFlush(PokerGame game) =>
        (game.Players.Where(HasFlush).FindPlayerWithHighestCard(), PokerHand.Flush);

    public static (Player Winner, PokerHand WinningHand) PlayerWithAFullHouse(PokerGame game) =>
        (game.Players.Where(HasFullHouse).FindPlayerWithHighestCard(), PokerHand.FullHouse);

    public static (Player Winner, PokerHand WinningHand) PlayerWithFourOfAKind(PokerGame game) =>
        (game.Players.SelectPlayersWithSameCards(4).FindPlayerWithHighestCard(), PokerHand.FourOfAKind);
    public static (Player Winner, PokerHand WinningHand) PlayerWithStraight(PokerGame game) =>
        (game.Players.Where(HasConsecutive).FindPlayerWithHighestCard(), PokerHand.StraightFlush);

    public static (Player Winner, PokerHand WinningHand) PlayerWithStraightFlush(PokerGame game) =>
        (game.Players.Where(HasStraightFlush).FindPlayerWithHighestCard(), PokerHand.StraightFlush);
}