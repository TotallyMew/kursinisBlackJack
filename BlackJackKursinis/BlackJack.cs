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
using System.ComponentModel;

namespace BlackJackKursinis
{
    public class BlackJack
    {
        private readonly Deck deck;
        private Dealer dealer;
        private readonly Player player;
        private double playerBet;
        private readonly InputOutput io;

        public BlackJack()
        {

            io = new InputOutput();
            deck = Deck.GetInstance(); // creational design pattern - singleton
            dealer = new Dealer();
            player = new Player();
        }
        private void executeTurn(ITurnStrategy strategy, Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            strategy.executeTurn(participant, deck, io, ref playerBet);
        }
        public void StartGame()
        {
            playerBet = 0;

            while (player.playerMoney <= 0)
            {
                player.playerMoney = io.readDouble("Welcome to BlackJack. How much money are you bringing to the table?");
            }

            while (playerBet <= 0 || playerBet > player.playerMoney)
            {
                playerBet = io.readDouble($"How much are you betting this hand? (Max: {player.playerMoney})");
            }

            player.playerMoney -= playerBet;

            dealer.hiddenCard = deck.drawCard();
            dealer.Hit(deck);

            io.displayMessage($"Current amount of money: {player.playerMoney}");
            io.displayMessage($"Current bet: {playerBet}");

            for (int i = 0; i < 2; i++)
            {
                player.Hit(deck);
            }

            io.displayCollection("Player's current hand", player.hand);
            io.displayMessage($"Player's current score: {player.getScore()}");
            io.displayCollection("Dealer's current hand", dealer.hand);
            io.displayMessage($"Dealer's current score: {dealer.getScore()}");

            ITurnStrategy playerTurnStrategy = new PlayerTurnStrategy();
            executeTurn(playerTurnStrategy, player, deck, io, ref playerBet);

            ITurnStrategy dealerTurnStrategy = new DealerTurnStrategy();
            executeTurn(dealerTurnStrategy, dealer, deck, io, ref playerBet);

           getWinner(player.getScore(), dealer.getScore());
        }


        public void getWinner(int playerScore, int dealerScore)
        {
            var winner = checkWinnerType(playerScore, dealerScore);
            double winSum = calculatePayout(winner, playerBet);
            player.playerMoney += winSum;
            displayResult(winner, playerScore, dealerScore);
            restartGame();
        }

        public void restartGame()
        {
            if (player.playerMoney == 0)
            {
                io.displayMessage("Unfortunately, you have lost everything and are unable to continue the game. Please come back later.");
                Environment.Exit(0);
            }

            io.displayMessage($"Current amount of money: {player.playerMoney}");
            bool playAgain = io.readYesNo("Do you want to play again");

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
                io.displayMessage("Thank you for playing! Goodbye.");
                Environment.Exit(0);
            }
        }

        public enum winnerType
        {
            Player,
            PlayerBlackjack,
            DealerBlackjack,
            Dealer,
            Tie,
            PlayerBust,
            DealerBust
        }

        public winnerType checkWinnerType(int playerScore, int dealerScore)
        {
            if (playerScore == GameConstants.blackJack && dealerScore != GameConstants.blackJack)
                return winnerType.PlayerBlackjack;

            if (dealerScore == GameConstants.blackJack && playerScore != GameConstants.blackJack)
                return winnerType.DealerBlackjack;

            if (playerScore == GameConstants.blackJack && dealerScore == GameConstants.blackJack)
                return winnerType.Tie;

            if (playerScore > GameConstants.blackJack)
                return winnerType.PlayerBust;

            if (dealerScore > GameConstants.blackJack)
                return winnerType.DealerBust;

            if (playerScore > dealerScore)
                return winnerType.Player;

            if (dealerScore > playerScore)
                return winnerType.Dealer;

            return winnerType.Tie;
        }
        public double calculatePayout(winnerType winnertype, double playerBet)
        {
            return winnertype switch
            {

                winnerType.Player => playerBet * 2,
                winnerType.PlayerBust =>0,
                winnerType.PlayerBlackjack => playerBet * 2.5,
                winnerType.DealerBust => playerBet *2,
                winnerType.Tie => playerBet,
                _ => 0,
            };
        }

        public void displayResult(winnerType winner, int playerScore, int dealerScore)
        {
            switch (winner)
            {
                case winnerType.PlayerBlackjack:
                    io.displayMessage("Player has Blackjack! Player wins!");
                    break;
                case winnerType.DealerBlackjack:
                    io.displayMessage("Dealer has Blackjack! Dealer wins!");
                    break;
                case winnerType.PlayerBust:
                    io.displayMessage("Bust! Player loses.");
                    break;
                case winnerType.DealerBust:
                    io.displayMessage("Bust! Dealer has lost, player wins!");
                    break;
                case winnerType.Player:
                    io.displayMessage($"Player wins with a score of {playerScore} against dealer's {dealerScore}.");
                    break;
                case winnerType.Dealer:
                    io.displayMessage($"Dealer wins with a score of {dealerScore} against player's {playerScore}.");
                    break;
                case winnerType.Tie:
                    io.displayMessage($"It's a tie with both scoring {playerScore}!");
                    break;
            }
        }
    }
}

