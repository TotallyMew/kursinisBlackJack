using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class DealerTurnStrategy : ITurnStrategy
    {
        public void ExecuteTurn(Participant participant, Deck deck, InputOutput io, ref double playerBet)
        {
            Dealer dealer = (Dealer)participant;
            io.DisplayMessage("Dealer's turn...");

            while (dealer.getScore() < 17)
            {
                dealer.Hit(deck);
                io.DisplayCollection("Dealer's hand", dealer.hand);
                io.DisplayMessage($"Dealer's score: {dealer.getScore()}");
            }

            if (dealer.getScore() > 21)
            {
                io.DisplayMessage("Bust! Dealer loses.");
            }
            else
            {
                io.DisplayMessage($"Dealer stands with score: {dealer.getScore()}");
            }
        }
    }

}
