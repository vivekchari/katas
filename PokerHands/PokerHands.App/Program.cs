using System;
using System.Collections.Generic;

using static PlayerExtensions;
using static PokerHandRules;

Console.WriteLine("Black:");
var player1Cards = Console.ReadLine();

Console.WriteLine("White:");
var player2Cards = Console.ReadLine();

var game = new PokerGame(new List<Player> {
                new Player("Black", player1Cards.ToCards()),
                new Player("White", player2Cards.ToCards())
            })
    .FindWinner(WithStraightFlush)
    .FindWinner(WithFourOfAKind)
    .FindWinner(WithAFullHouse)
    .FindWinner(WithAFlush)
    .FindWinner(WithStraight)
    .FindWinner(WithThreeOfAKind)
    .FindWinner(WithTwoPairs)
    .FindWinner(WithAPair)
    .FindWinner(WithHighCard);

if (game.Winner != null)
    Console.WriteLine($"{game.Winner.Name} wins with {game.WinningHand}");
else
    Console.WriteLine($"Tie");
