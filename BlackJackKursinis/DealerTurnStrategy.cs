using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class DealerTurnStrategy : ITurnStrategy
    {
        public void executeTurn(Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            Dealer dealer = (Dealer)participant;
            io.displayMessage("Dealer's turn...");

            while (dealer.getScore() < GameConstants.dealerMaxScore)
            {
                dealer.Hit(deck);
                io.displayCollection("Dealer's hand", dealer.hand);
                io.displayMessage($"Dealer's score: {dealer.getScore()}");
            }
                io.displayMessage($"Dealer stands with score: {dealer.getScore()}");
        }
    }

}
