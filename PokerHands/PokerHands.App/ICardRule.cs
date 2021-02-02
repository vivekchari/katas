using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    public string Name { get; init; }
    public IEnumerable<Card> Hand { get; init; }

    public Player(string name, IEnumerable<Card> cards)
    {
        Name = name;
        Hand = cards;
    }
}

public enum PokerHand
{
    HighCard,
    TwoOfAKind,
    TwoPair,
    ThreeOfAKind,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush
}

public interface ICardRule
{
    Player FindWinner(Player player1, Player player2);
}


public class HighCardRule : ICardRule
{
    public Player FindWinner(Player player1, Player player2)
    {
        var hands = player1.Hand.Select(h => (player: player1.Name, card: h)).Concat(player2.Hand.Select(h => (player: player2.Name, card: h)));
        var highCardPlayerName = hands
                                    .GroupBy(h => h.card.value)
                                    .Where(g => g.Count() < 2)
                                    .OrderByDescending(g => g.First().card.value)
                                    .First()
                                    .First()
                                    .player;
        return player1.Name == highCardPlayerName ? player1 : player2;
    }
}


public class PairOfTwoCardRule : ICardRule
{
    public Player FindWinner(Player player1, Player player2)
    {
        var player1Pair = player1.Hand.GroupBy(g => g.value).FirstOrDefault(g => g.Count() == 2)?.ToList();
        var player2Pair = player2.Hand.GroupBy(g => g.value).FirstOrDefault(g => g.Count() == 2)?.ToList();

        var hands = player1.Hand.Select(h => (player: player1.Name, card: h)).Concat(player2.Hand.Select(h => (player: player2.Name, card: h)));
        var pairCardPlayer = hands.GroupBy(h => h.card.value).Where(g => g.Count() == 2).SingleOrDefault();
        return player1.Name == pairCardPlayer?.First().player ? player1 : player2;
    }
}