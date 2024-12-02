using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Participant
    {

        public List<Card> hand;
        public int score;
        public int aceCount;

        protected Participant()
        {
            hand = new List<Card>();
            score = 0;
            aceCount = 0;
        }

        public void Hit(Deck deck)
        {

            Card card = deck.drawCard();
            hand.Add(card);
            score += card.getValue();
            aceCount += card.isAce() ? 1 : 0;

            acesCheck();


        }
        public void acesCheck()
        {
            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount -= 1;
            }
        }

    }
}
