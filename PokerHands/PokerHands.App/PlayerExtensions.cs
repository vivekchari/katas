using System;
using System.Collections.Generic;
using System.Linq;

public static class PlayerExtensions
{
    public static Func<Player, bool> HasAPair = (Player p) => p.HasSameCards(2);
    public static Func<Player, bool> HasTwoPairs = (Player p) => p.HasASetOfSameCards(2, 2);
    public static Func<Player, bool> HasThreeOfAKind = (Player p) => p.HasSameCards(3);
    public static Func<Player, bool> HasFourOfAKind = (Player p) => p.HasSameCards(4);
    public static Func<Player, bool> HasFlush = (Player p) => p.HasSameSuites(5);
    public static Func<Player, bool> HasFullHouse = (Player p) => p.HasSameCards(2) && p.HasSameCards(3);
    public static Func<Player, bool> HasConsecutive = (Player p) => p.Hand.ToList().OrderBy(c => c.value).Aggregate((a, c) => a?.value + 1 == c?.value ? c : null) != null;
    public static Func<Player, bool> HasStraightFlush = (Player p) => HasConsecutive(p) && HasFlush(p);

    public static bool HasSameCards(this Player player, int numberOfCards) =>
        player.SelectCardsWithSameValue(numberOfCards).Any();

    public static bool HasSameSuites(this Player player, int numberOfCards) =>
       player.SelectCardsWithSameSuite(numberOfCards).Any();

    public static bool HasASetOfSameCards(this Player player, int numberOfCards, int numberOfSets) =>
        player.Hand.GroupBy(h => h.value).Count(g => g.Count() == numberOfCards) == numberOfSets;

    public static IEnumerable<Card> SelectCardsWithSameValue(this Player player, int numberOfCards) =>
        player.Hand.GroupBy(h => h.value).Where(g => g.Count() == numberOfCards).SelectMany(g => g.ToList());
    public static IEnumerable<Card> SelectCardsWithSameSuite(this Player player, int numberOfCards) =>
       player.Hand.GroupBy(h => h.suite).Where(g => g.Count() == numberOfCards).SelectMany(g => g.ToList());

    public static Player SelectPlayerWithHighCard(this IEnumerable<Player> players) => players?
                .Select(p =>
                    (player: p,
                    topCardValue: p.Hand.Any() ? p.Hand.Max(p => p.value) : 0))
                .GroupBy(g => g.topCardValue)
                .Where(g => g.Count() == 1)?
                .OrderByDescending(g => g.Key)
                .FirstOrDefault()?
                .FirstOrDefault()
                .player;

    public static Func<Player, int, Player> PlayersWithSameCards => (p, numberOfCards) => new Player(
                                    p.Name,
                                    p.SelectCardsWithSameValue(numberOfCards));
    public static Func<Player, Player> PlayersWithAPair => (p) => PlayersWithSameCards(p, 2);
    public static Func<Player, Player> PlayersWithThreeOfAKind => (p) => PlayersWithSameCards(p, 3);
    public static Func<Player, Player> PlayersWithFourOfAKind => (p) => PlayersWithSameCards(p, 4);
    public static IEnumerable<Player> SelectPlayersWithUniqueCards(this IEnumerable<Player> players) =>
                players
               .Select(p => new Player(
                                    p.Name,
                                    p.Hand.Except(players.Except(new List<Player> { p }).SelectMany(p => p.Hand))
                            ));
}
