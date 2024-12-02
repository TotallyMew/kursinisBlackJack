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


            playerBet = 0;

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
            Console.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                player.Hit(deck);
            }

            Console.WriteLine("Player's current hand:");
            foreach (var playerCard in player.hand)
            {
                Console.WriteLine(playerCard);
                    
            }
            Console.WriteLine("Player's current score: " + player.score);
            Console.WriteLine();
            Console.WriteLine("Dealer's current hand:");
            foreach (var dealerCard in dealer.hand)
            {
                Console.WriteLine(dealerCard);
            }
            Console.WriteLine("Dealer's current score: " + dealer.score);
            Console.WriteLine();


            playerTurn();

            dealerTurn();

            checkWinner(player.score, dealer.score);

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
                player.score = 0;
                player.hand.Clear();
                player.aceCount = 0;

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
            int amountOfTurns = 0;
            if (player.score == 21)
            {
                Console.WriteLine("BlackJack! Player wins!");
                playerBet *= 2.5;
                player.playerMoney += playerBet;
                restartGame();
            }
            while (player.score < 21)
            {
                Console.WriteLine("Press H to hit, S to stay or D to double down");
                string input = Console.ReadLine().ToUpper();
                amountOfTurns++;
                if (input == "H")
                {
                    player.Hit(deck);
                    Console.WriteLine();
                    Console.WriteLine("Player's new hand: ");
                    foreach (var playerCard in player.hand)
                    {
                        Console.WriteLine(playerCard);
                    }
                    Console.WriteLine("Player's score: " + player.score);
                }
                else if (input == "S")
                {
                    Console.WriteLine();
                    Console.WriteLine("Player stands with score: " + player.score);
                    break;
                }
                else if(input == "D")
                {
                    if(amountOfTurns > 1)
                    {
                        Console.WriteLine("You cannot double down");
                        continue;
                    }
                    if(player.playerMoney > playerBet*2)
                    {
                        Console.WriteLine("You cannot double down");
                        continue;
                    }
                    player.playerMoney -= playerBet;
                    playerBet *= 2;
                    player.Hit(deck);
                    Console.WriteLine("Player's new hand: ");
                    foreach (var playerCard in player.hand)
                    {
                        Console.WriteLine(playerCard);
                    }
                    Console.WriteLine("Player's score: " + player.score);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'H' to hit or 'S' to stand.");
                }
            }
            if (player.score > 21)
            {

                Console.WriteLine("Bust! Player loses");
                restartGame();
            }


        }


        private void dealerTurn()
        {
            Console.WriteLine("Dealer's turn.");
            Console.WriteLine();
            dealer.hand.Add(dealer.hiddenCard);
            Console.WriteLine("Dealer reveals hidden card:");
            Console.WriteLine(dealer.hiddenCard);
            dealer.score += dealer.hiddenCard.getValue();
            dealer.aceCount += dealer.hiddenCard.isAce() ? 1 : 0;
            Console.WriteLine("Dealer's hand:");

            foreach (var card in dealer.hand)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine();

            Console.WriteLine("Dealer's score after revealing hidden card: " + dealer.score);
            Console.WriteLine();
            Thread.Sleep(3000);
            while (dealer.score < 17)
            {


                Card newCard = deck.drawCard();
                //dealer.hiddenCard.Add(newCard);
                dealer.score += newCard.getValue();
                dealer.aceCount += newCard.isAce() ? 1 : 0;

                Console.WriteLine("Dealer draws: " + newCard);
                Console.WriteLine("Dealer's current score: " + dealer.score);
                Console.WriteLine();
                Thread.Sleep(2000);
            }


            if (dealer.score > 21)
            {
                Console.WriteLine("Bust! Dealer has lost, player wins!");
                player.playerMoney += (2 * playerBet);
                restartGame();
            }
            Console.WriteLine();
            Console.WriteLine("Dealer's final score: " + dealer.score);


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