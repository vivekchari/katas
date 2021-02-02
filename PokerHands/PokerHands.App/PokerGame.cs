using System.Collections.Generic;
using System.Linq;
using static PlayerExtensions;

public class PokerGame
{
    public Player Winner { get; private set; }
    public PokerHand WinningHand { get; private set; }

    public bool HasWinner => Winner != null;

    private IEnumerable<Player> _players = new List<Player>();
    public IReadOnlyCollection<Player> Players => _players.ToList().AsReadOnly();
    public PokerGame(IEnumerable<Player> players = null)
    {
        Winner = null;
        _players = players ?? new List<Player>();
    }

    public PokerGame SetWinner(Player winner, PokerHand winningHand)
    {
        this.Winner = winner;
        this.WinningHand = winningHand;
        return this;
    }
}
