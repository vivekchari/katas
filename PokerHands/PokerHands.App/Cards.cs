using System;
using System.Collections.Generic;
using System.Linq;

public record Card(int value, Suite suite)
{
    public virtual bool Equals(Card card) => card?.value == this.value;
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.value);
        return hashCode.ToHashCode();
    }
}

public static class CardExtensions
{
    private static Suite ToSuite(string c) => c switch
    {
        "H" => Suite.Hearts,
        "D" => Suite.Diamond,
        "S" => Suite.Spade,
        "C" => Suite.Club,
        _ => throw new Exception($"Invalid Suite: {c}")
    };

    private static object ParseCardValue(string c) => int.TryParse(c.ToString(), out var i) ? i : c.ToString();

    private static int ToValue(object c) => ParseCardValue(c.ToString()) switch
    {
        int i when i >= 2 && i <= 9 => i,
        "T" => 10,
        "J" => 11,
        "Q" => 12,
        "K" => 13,
        "A" => 14,
        _ => throw new Exception($"Invalid card value: {c}")
    };

    public static IEnumerable<Card> ToCards(this string input) =>
        input
            .Split(" ")
            .Select(c => c.Trim())
            .Select(c => new Card(ToValue(c.ElementAt(0)), ToSuite(c.ElementAt(1).ToString())));
}

public enum Suite
{
    Hearts,
    Diamond,
    Spade,
    Club
}