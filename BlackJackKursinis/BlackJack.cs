using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

using System;
using System.Threading;

namespace BlackJackKursinis
{
    internal class BlackJack
    {
        private readonly Deck deck;
        private Dealer dealer;
        private readonly Player player;
        private double playerBet;
        private const int blackJack = 21;
        private const int dealerMaxScore = 17;

        private readonly InputOutput io;

        public BlackJack()
        {

            io = new InputOutput();
            deck = Deck.GetInstance(); // creational design pattern - singleton
            dealer = new Dealer();
            player = new Player();
            StartGame();
        }
        private void ExecuteTurn(ITurnStrategy strategy, Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            strategy.ExecuteTurn(participant, deck, io, ref playerBet);
        }
        public void StartGame()
        {
            playerBet = 0;

            while (player.playerMoney <= 0)
            {
                player.playerMoney = io.ReadDouble("Welcome to BlackJack. How much money are you bringing to the table?");
            }

            while (playerBet <= 0 || playerBet > player.playerMoney)
            {
                playerBet = io.ReadDouble($"How much are you betting this hand? (Max: {player.playerMoney})");
            }

            player.playerMoney -= playerBet;

            dealer.hiddenCard = deck.drawCard();
            dealer.Hit(deck);

            io.DisplayMessage($"Current amount of money: {player.playerMoney}");
            io.DisplayMessage($"Current bet: {playerBet}");

            for (int i = 0; i < 2; i++)
            {
                player.Hit(deck);
            }

            io.DisplayCollection("Player's current hand", player.hand);
            io.DisplayMessage($"Player's current score: {player.getScore()}");
            io.DisplayCollection("Dealer's current hand", dealer.hand);
            io.DisplayMessage($"Dealer's current score: {dealer.getScore()}");

            ITurnStrategy playerTurnStrategy = new PlayerTurnStrategy();
            ExecuteTurn(playerTurnStrategy, player, deck, io, ref playerBet);

            ITurnStrategy dealerTurnStrategy = new DealerTurnStrategy();
            ExecuteTurn(dealerTurnStrategy, dealer, deck, io, ref playerBet);

            CheckWinner(player.getScore(), dealer.getScore());
        }


        public void RestartGame()
        {
            if (player.playerMoney == 0)
            {
                io.DisplayMessage("Unfortunately, you have lost everything and are unable to continue the game. Please come back later.");
                Environment.Exit(0);
            }

            io.DisplayMessage($"Current amount of money: {player.playerMoney}");
            bool playAgain = io.ReadYesNo("Do you want to play again");

            if (playAgain)
            {
                deck.fillDeck();
                dealer = new Dealer();
                player.updateScore(0);
                player.hand.Clear();
                player.aceCount = 0;

                Console.Clear();
                StartGame();
            }
            else
            {
                io.DisplayMessage("Thank you for playing! Goodbye.");
                Environment.Exit(0);
            }
        }



        private void CheckWinner(int playerScore, int dealerScore)
        {
            if (playerScore > blackJack)
                io.DisplayMessage("Bust! Player loses.");
            else if (dealerScore > blackJack)
            {
                io.DisplayMessage("Bust! Dealer has lost, player wins!");
                player.playerMoney += (2 * playerBet);
            }
            else if (playerScore == blackJack && dealerScore != blackJack)
            {
                io.DisplayMessage("Player has Blackjack! Player wins!");
                player.playerMoney += (2.5 * playerBet);
            }
            else if (dealerScore == blackJack && playerScore != blackJack)
                io.DisplayMessage("Dealer has Blackjack! Dealer wins!");
            else if (playerScore == blackJack && dealerScore == blackJack)
            {
                io.DisplayMessage("Both have Blackjack! It's a tie!");
                player.playerMoney += playerBet;
            }
            else if (playerScore > dealerScore)
            {
                io.DisplayMessage($"Player wins with a score of {playerScore} against dealer's {dealerScore}.");
                player.playerMoney += (playerBet * 2);
            }
            else if (dealerScore > playerScore)
                io.DisplayMessage($"Dealer wins with a score of {dealerScore} against player's {playerScore}.");
            else
            {
                io.DisplayMessage($"It's a tie with both scoring {playerScore}!");
                player.playerMoney += playerBet;
            }
            RestartGame();
        }
    }
}

