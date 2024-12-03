using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Player : Participant
    {

        public double playerMoney;

        public Player()
        {
            playerMoney = 0;
        }


        //public override void TakeTurn(Deck deck, InputOutput io, ref double playerBet)
        //{
        //    int amountOfTurns = 0;

        //    //if (score == 21)
        //    //{
        //    //    io.DisplayMessage("BlackJack! Player wins!");
        //    //    playerBet *= 2.5;
        //    //    playerMoney += playerBet;
        //    //}

        //    while (score < 21)
        //    {
        //        string input = io.PromptMessage("Press H to hit, S to stay, or D to double down").ToUpper();
        //        amountOfTurns++;

        //        if (input == "H")
        //        {
        //            Hit(deck);
        //            io.DisplayCollection("Player's new hand", hand);
        //            io.DisplayMessage($"Player's score: {score}");
        //        }
        //        else if (input == "S")
        //        {
        //            io.DisplayMessage($"Player stands with score: {score}");
        //            break;
        //        }
        //        else if (input == "D")
        //        {
        //            if (amountOfTurns > 1 || playerMoney < playerBet * 2)
        //            {
        //                io.DisplayMessage("You cannot double down.");
        //                continue;
        //            }

        //            playerMoney -= playerBet;
        //            playerBet *= 2;
        //            Hit(deck);
        //            io.DisplayCollection("Player's new hand", hand);
        //            io.DisplayMessage($"Player's score: {score}");
        //            break;
        //        }
        //        else
        //        {
        //            io.DisplayMessage("Invalid input. Please enter 'H' to hit, 'S' to stay, or 'D' to double down.");
        //        }
        //    }

            //if (score > 21)
            //{
            //    io.DisplayMessage("Bust! Player loses.");
            //}
        //}


    }
}
