using System;
using System.Collections.Generic;
using System.Linq;

public static class PlayerExtensions
{
    public static Func<Player, bool> HasFlush = (Player p) => p.HasSameSuites(5);
    public static Func<Player, bool> HasFullHouse = (Player p) => p.HasSameCards(2) && p.HasSameCards(3);
    public static Func<Player, bool> HasTwoPairs = (Player p) => p.HasASetOfSameCards(2, 2);
    public static Func<Player, bool> HasConsecutive = (Player p) => p.Hand.ToList().OrderBy(c => c.value).Aggregate((a, c) => a?.value + 1 == c?.value ? c : null) != null;
    public static Func<Player, bool> HasStraightFlush = (Player p) => HasConsecutive(p) && HasFlush(p);
    public static Player SelectPlayerWithHighCard(this IEnumerable<Player> players)
    {
        var highestCard = players?
                .Select(p =>
                    (player: p,
                    topCardValue: p.Hand.Any() ? p.Hand.Max(p => p.value) : 0))
                .GroupBy(g => g.topCardValue)
                .Where(g => g.Count() == 1)?
                .OrderByDescending(g => g.Key)
                .FirstOrDefault()?
                .FirstOrDefault();
        return highestCard?.player;
    }

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

    public static IEnumerable<Player> SelectPlayersWithSameCards(this IEnumerable<Player> players, int numberOfCards) =>
                players
                .Where(p => p.HasSameCards(numberOfCards))
                .Select(p => new Player(p.Name, p.SelectCardsWithSameValue(numberOfCards)));

    public static IEnumerable<Player> SelectPlayersWithUniqueCards(this IEnumerable<Player> players) =>
                players
               .Select(p => new Player(
                                    p.Name,
                                    p.Hand.Except(players.Except(new List<Player> { p }).SelectMany(p => p.Hand))
                            ));
}
