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
        private Deck deck;
        private Dealer dealer;
        private Player player;
        private double playerBet;
        private const int blackJack = 21;
        private const int dealerMaxScore = 17;

        private readonly InputOutput io;

        public BlackJack()
        {
            io = new InputOutput();
            deck = new Deck();
            dealer = new Dealer();
            player = new Player();
            StartGame();
        }

        // Game Logic
        public void StartGame()
        {
            playerBet = 0;

            while (player.playerMoney <= 0)
            {
                player.playerMoney = io.ReadDouble("Welcome to BlackJack. How much money are you bringing to the table");
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
            io.DisplayMessage($"Player's current score: {player.score}");
            io.DisplayCollection("Dealer's current hand", dealer.hand);
            io.DisplayMessage($"Dealer's current score: {dealer.score}");

            PlayerTurn();
            DealerTurn();
            CheckWinner(player.score, dealer.score);
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
                deck = new Deck();
                dealer = new Dealer();
                player.score = 0;
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

        public void PlayerTurn()
        {
            int amountOfTurns = 0;

            if (player.score == blackJack)
            {
                io.DisplayMessage("BlackJack! Player wins!");
                playerBet *= 2.5;
                player.playerMoney += playerBet;
                RestartGame();
            }

            while (player.score < blackJack)
            {
                string input = io.PromptMessage("Press H to hit, S to stay, or D to double down").ToUpper();
                amountOfTurns++;

                if (input == "H")
                {
                    player.Hit(deck);
                    io.DisplayCollection("Player's new hand", player.hand);
                    io.DisplayMessage($"Player's score: {player.score}");
                }
                else if (input == "S")
                {
                    io.DisplayMessage($"Player stands with score: {player.score}");
                    break;
                }
                else if (input == "D")
                {
                    if (amountOfTurns > 1 || player.playerMoney < playerBet * 2)
                    {
                        io.DisplayMessage("You cannot double down.");
                        continue;
                    }

                    player.playerMoney -= playerBet;
                    playerBet *= 2;
                    player.Hit(deck);
                    io.DisplayCollection("Player's new hand", player.hand);
                    io.DisplayMessage($"Player's score: {player.score}");
                    break;
                }
                else
                {
                    io.DisplayMessage("Invalid input. Please enter 'H' to hit, 'S' to stay, or 'D' to double down.");
                }
            }

            if (player.score > blackJack)
            {
                io.DisplayMessage("Bust! Player loses.");
                RestartGame();
            }
        }

        private void DealerTurn()
        {
            io.DisplayMessage("Dealer's turn");
            dealer.hand.Add(dealer.hiddenCard);
            io.DisplayMessage("Dealer reveals hidden card:");
            io.DisplayMessage(dealer.hiddenCard.ToString());
            dealer.score += dealer.hiddenCard.getValue();
            dealer.aceCount += dealer.hiddenCard.isAce() ? 1 : 0;

            io.DisplayCollection("Dealer's hand", dealer.hand);
            io.DisplayMessage($"Dealer's score after revealing hidden card: {dealer.score}");
            Thread.Sleep(3000);

            while (dealer.score < dealerMaxScore)
            {
                Card newCard = deck.drawCard();
                dealer.score += newCard.getValue();
                dealer.aceCount += newCard.isAce() ? 1 : 0;

                io.DisplayMessage($"Dealer draws: {newCard}");
                io.DisplayMessage($"Dealer's current score: {dealer.score}");
                Thread.Sleep(2000);
            }

            if (dealer.score > blackJack)
            {
                io.DisplayMessage("Bust! Dealer has lost, player wins!");
                player.playerMoney += (2 * playerBet);
                RestartGame();
            }

            io.DisplayMessage($"Dealer's final score: {dealer.score}");
        }

        private void CheckWinner(int playerScore, int dealerScore)
        {
            if (playerScore == blackJack && dealerScore != blackJack)
            {
                io.DisplayMessage("Player has Blackjack! Player wins!");
                player.playerMoney += (2.5 * playerBet);
            }
            else if (dealerScore == blackJack && playerScore != blackJack)
            {
                io.DisplayMessage("Dealer has Blackjack! Dealer wins!");
            }
            else if (playerScore == blackJack && dealerScore == blackJack)
            {
                io.DisplayMessage("Both have Blackjack! It's a tie!");
                player.playerMoney += playerBet;
            }
            else if (playerScore > dealerScore && playerScore < blackJack)
            {
                io.DisplayMessage($"Player wins with a score of {playerScore} against dealer's {dealerScore}.");
                player.playerMoney += (playerBet * 2);
            }
            else if (dealerScore > playerScore && dealerScore < blackJack)
            {
                io.DisplayMessage($"Dealer wins with a score of {dealerScore} against player's {playerScore}.");
            }
            else
            {
                io.DisplayMessage($"It's a tie with both scoring {playerScore}!");
                player.playerMoney += playerBet;
            }

            RestartGame();
        }
    }
}

