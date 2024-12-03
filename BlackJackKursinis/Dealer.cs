using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Dealer : Participant
    {

        public Card hiddenCard;

        public Dealer() 
        {


        }


    //    public override void TakeTurn(Deck deck, InputOutput io, ref double playerBet)
    //    {
    //        io.DisplayMessage("Dealer's turn");
    //        hand.Add(hiddenCard);
    //        io.DisplayMessage("Dealer reveals hidden card:");
    //        io.DisplayMessage(hiddenCard.ToString());
    //        score += hiddenCard.getValue();
    //        aceCount += hiddenCard.isAce() ? 1 : 0;

    //        io.DisplayCollection("Dealer's hand", hand);
    //        io.DisplayMessage($"Dealer's score after revealing hidden card: {score}");
    //        Thread.Sleep(3000);

    //        while (score < 17)
    //        {
    //            Card newCard = deck.drawCard(); // drawCard galetu handlint score ir isace?
    //            score += newCard.getValue();
    //            aceCount += newCard.isAce() ? 1 : 0;

    //            io.DisplayMessage($"Dealer draws: {newCard}");
    //            io.DisplayMessage($"Dealer's current score: {score}");
    //            Thread.Sleep(2000);
    //        }


    //        io.DisplayMessage($"Dealer's final score: {score}");
    //    }
    }

}
