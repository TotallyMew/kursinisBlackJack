using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class PlayerTurnStrategy : ITurnStrategy
    {
        public void ExecuteTurn(Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            Player player = (Player)participant;
            io.DisplayMessage("Player's turn...");

            while (player.getScore() < 21)
            {
                string input = io.PromptMessage("Press H to hit, S to stand, or D to double down").ToUpper();

                if (input == "H")
                {
                    player.Hit(deck);
                    io.DisplayCollection("Player's hand", player.hand);
                    io.DisplayMessage($"Player's score: {player.getScore()}");
                }
                else if (input == "S")
                {
                    io.DisplayMessage($"Player stands with score: {player.getScore()}");
                    break;
                }
                else if (input == "D" && player.playerMoney >= playerBet * 2)
                {
                    player.playerMoney -= playerBet;
                    playerBet *= 2;
                    player.Hit(deck);
                    io.DisplayCollection("Player's hand", player.hand);
                    io.DisplayMessage($"Player's score: {player.getScore()}");
                    break;
                }
                else
                {
                    io.DisplayMessage("Invalid input or insufficient funds for double down.");
                }
            }

            if (player.getScore() > 21)
            {
                io.DisplayMessage("Bust! Player loses.");
            }
        }
    }

}
