using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

namespace BlackJackKursinis
{
    internal class BlackJack
    {

        private Deck deck;
        private Dealer dealer;
        private Player player;
        private double playerBet;

        public BlackJack()
        {
            deck = new Deck();
            dealer = new Dealer();
            player = new Player();
            startGame();

        }

        public void startGame()
        {


            while (player.playerMoney <= 0)
            { 
                Console.WriteLine("Welcome to BlackJack. How much money are you bringing to the table?");
                string input = Console.ReadLine();
                player.playerMoney = double.Parse(input);
            }

            while(playerBet <=0 || playerBet > player.playerMoney) {
            Console.WriteLine("How much are you betting this hand?");
            string playerBetString = Console.ReadLine();
            playerBet = double.Parse(playerBetString);
                }

            player.playerMoney -= playerBet;
            
            dealer.hiddenCard = deck.drawCard();
            dealer.Hit(deck);

            Console.WriteLine();
            Console.WriteLine("Current amount of money: " + player.playerMoney);
            Console.WriteLine("Current bet: " + playerBet);


            for (int i = 0; i < 2; i++)
            {
                player.Hit(deck);
            }

            Console.WriteLine("Player's current hand:");
            foreach (var playerCard in player.playerHand)
            {
                Console.WriteLine(playerCard);
                    
            }
            Console.WriteLine("Player's current score: " + player.playerScore);
            Console.WriteLine();
            Console.WriteLine("Dealer's current hand:");
            foreach (var dealerCard in dealer.dealerHand)
            {
                Console.WriteLine(dealerCard);
            }
            Console.WriteLine("Dealer's current score: " + dealer.dealerScore);
            Console.WriteLine();


            playerTurn();

            dealerTurn();

            checkWinner(player.playerScore, dealer.dealerScore);

        }
        public void restartGame()
        {
            if(player.playerMoney == 0)
            {
                Console.WriteLine("Unfortunately you have lost everything and are unable to continue the game, please comeback later.");
                Environment.Exit(0);
            }


            Console.WriteLine("Current amount of money: " + player.playerMoney);
            Console.WriteLine("Do you want to play again? (Y/N)");
            string input = Console.ReadLine().ToUpper();

            if (input == "Y")
            {
  
                deck = new Deck();
                dealer = new Dealer();
                player.playerScore = 0;
                player.playerHand.Clear();
                player.playerAceCount = 0;

                Console.Clear(); 
                startGame(); 
            }
            else if (input == "N")
            {
                Console.WriteLine("Thank you for playing! Goodbye.");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'Y' to play again or 'N' to exit.");
                restartGame(); 
            }
        }

        public void playerTurn()
        {
            if (player.playerScore == 21)
            {
                Console.WriteLine("BlackJack! Player wins!");
                playerBet *= 2.5;
                player.playerMoney += playerBet;
                restartGame();
            }
            while (player.playerScore < 21)
            {
                Console.WriteLine("Press H to hit, S to stay or D to double down");
                string input = Console.ReadLine().ToUpper();

                if (input == "H")
                {
                    player.Hit(deck);
                    Console.WriteLine();
                    Console.WriteLine("Player's new hand: ");
                    foreach (var playerCard in player.playerHand)
                    {
                        Console.WriteLine(playerCard);
                    }
                    Console.WriteLine("Player's score: " + player.playerScore);
                }
                else if (input == "S")
                {
                    Console.WriteLine();
                    Console.WriteLine("Player stands with score: " + player.playerScore);
                    break;
                }
                else if(input == "D")
                {
                    player.playerMoney -= playerBet;
                    playerBet *= 2;
                    player.Hit(deck);
                    Console.WriteLine("Player's new hand: ");
                    foreach (var playerCard in player.playerHand)
                    {
                        Console.WriteLine(playerCard);
                    }
                    Console.WriteLine("Player's score: " + player.playerScore);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'H' to hit or 'S' to stand.");
                }
            }
            if (player.playerScore > 21)
            {

                Console.WriteLine("Bust! Player loses");
                restartGame();
            }


        }


        private void dealerTurn()
        {
            Console.WriteLine("Dealer's turn.");
            Console.WriteLine();
            dealer.dealerHand.Add(dealer.hiddenCard);
            Console.WriteLine("Dealer reveals hidden card:");
            Console.WriteLine(dealer.hiddenCard);
            dealer.dealerScore += dealer.hiddenCard.getValue();
            dealer.dealerAceCount += dealer.hiddenCard.isAce() ? 1 : 0;
            Console.WriteLine("Dealer's hand:");

            foreach (var card in dealer.dealerHand)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine();

            Console.WriteLine("Dealer's score after revealing hidden card: " + dealer.dealerScore);

            Thread.Sleep(3000);
            while (dealer.dealerScore < 17)
            {


                Card newCard = deck.drawCard();
                dealer.dealerHand.Add(newCard);
                dealer.dealerScore += newCard.getValue();
                dealer.dealerAceCount += newCard.isAce() ? 1 : 0;

                Console.WriteLine("Dealer draws: " + newCard);
                Console.WriteLine("Dealer's current score: " + dealer.dealerScore);
                Thread.Sleep(2000);
            }


            if (dealer.dealerScore > 21)
            {
                Console.WriteLine("Bust! Dealer has lost, player wins!");
                player.playerMoney += (2 * playerBet);
                restartGame();
            }
            Console.WriteLine("Dealer's final score: " + dealer.dealerScore);


        }

        private void checkWinner(int playerScore, int dealerScore)
        {

            if (playerScore == 21 && dealerScore != 21)
            {
                Console.WriteLine("Player has Blackjack! Player wins!");
                player.playerMoney += (2.5 * playerBet);

            }
            else if (dealerScore == 21 && playerScore != 21)
            {
                Console.WriteLine("Dealer has Blackjack! Dealer wins!");
                

            }
            else if (playerScore == 21 && dealerScore == 21)
            {
                Console.WriteLine("Both have Blackjack! It's a tie!");
                player.playerMoney += playerBet;

            }
            else if (playerScore > dealerScore && playerScore < 21)
            {
                Console.WriteLine("Player wins with a score of " + playerScore + " against dealer's " + dealerScore + ".");
                player.playerMoney += (playerBet * 2);

            }
            else if (dealerScore > playerScore && dealerScore<21)
            {
                Console.WriteLine("Dealer wins with a score of " + dealerScore + " against player's " + playerScore + ".");
               
            }
            else
            {
                Console.WriteLine("It's a tie with both scoring " + playerScore + "!");
                player.playerMoney += playerBet;
            }

            restartGame();
        }

    }
}