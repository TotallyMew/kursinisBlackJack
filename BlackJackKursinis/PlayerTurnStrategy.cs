using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class PlayerTurnStrategy : ITurnStrategy
    {
        public void executeTurn(Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            Player player = (Player)participant;
            io.displayMessage("Player's turn...");

            while (player.getScore() < GameConstants.blackJack)
            {
                string input = io.getInput("Press H to hit, S to stand, or D to double down").ToUpper();

                if (input == "H")
                {
                    player.Hit(deck);
                    io.displayCollection("Player's hand", player.hand);
                    io.displayMessage($"Player's score: {player.getScore()}");
                }
                else if (input == "S")
                {
                    io.displayMessage($"Player stands with score: {player.getScore()}");
                    break;
                }
                else if (input == "D" && player.playerMoney >= playerBet * 2)
                {
                    player.playerMoney -= playerBet;
                    playerBet *= 2;
                    player.Hit(deck);
                    io.displayCollection("Player's hand", player.hand);
                    io.displayMessage($"Player's score: {player.getScore()}");
                    break;
                }
                else
                {
                    io.displayMessage("Invalid input or insufficient funds for double down.");
                }
            }

            if (player.getScore() > GameConstants.blackJack)
            {
                io.displayMessage("Bust! Player loses.");
            }
        }
    }

}
