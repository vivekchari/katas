using System;
using System.Linq;

public static class PokerGameExtension
{
    public static PokerGame FindWinner(this PokerGame game, Func<PokerGame, (Player Winnner, PokerHand WinningHand)> ruleFunc)
    {
        if (game.HasWinner) return game;

        var result = ruleFunc(game);
        if (result.Winnner != null) game.SetWinner(result.Winnner, result.WinningHand);
        return game;
    }


}
