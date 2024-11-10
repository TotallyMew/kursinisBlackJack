using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Player
    {


        public List<Card> playerHand;
        public int playerScore;
        public int playerAceCount;
        public double playerMoney;

        public Player()
        {

            playerHand = new List<Card>();
            playerScore = 0;
            playerAceCount = 0;
            playerMoney = 0;
        }


        public void Hit(Deck deck)
        {

            Card card = deck.drawCard();
            playerHand.Add(card);
            playerScore += card.getValue();
            playerAceCount += card.isAce() ? 1 : 0;

            acesCheck();

        }

        public void acesCheck()
        {
            while(playerScore > 21 && playerAceCount > 0)
            {
                playerScore -= 10;
                playerAceCount -= 1;
            }
        }


    }
}
