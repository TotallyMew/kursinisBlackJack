using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Dealer
    {

        public Card hiddenCard;
        public List<Card> dealerHand;
        public int dealerScore;
        public int dealerMoney;
        public int dealerAceCount;

        public Dealer() 
        {

            dealerHand = new List<Card>();
            dealerScore = 0;
            dealerAceCount = 0;

        }




        public void Hit(Deck deck)
        {

            Card card = deck.drawCard();
            dealerHand.Add(card);
            dealerScore += card.getValue();
            dealerAceCount += card.isAce() ? 1 : 0;

            acesCheck();


        }
        public void acesCheck()
        {
            while (dealerScore > 21 && dealerAceCount > 0)
            {
                dealerScore -= 10;
                dealerAceCount -= 1;
            }
        }
    }
}
