using System.Collections.Generic;

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
