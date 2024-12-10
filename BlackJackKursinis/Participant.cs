using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class Participant
    {

        public List<Card> hand;
        private int score {  get; set; }
        public int aceCount;

        protected Participant()
        {
            hand = new List<Card>();
            score = 0;
            aceCount = 0;
        }

        public int getScore() { return score; }
        public void updateScore(int newScore) { score = newScore; }

        public void Hit(Deck deck)
        {

            Card card = deck.drawCard();
            hand.Add(card);
            score += card.getValue();
            aceCount += card.isAce() ? 1 : 0;

            acesCheck();


        }
        protected void acesCheck()
        {
            while (score > GameConstants.blackJack && aceCount > 0)
            {
                score -= 10;
                aceCount -= 1;
            }
        }

    }
}
