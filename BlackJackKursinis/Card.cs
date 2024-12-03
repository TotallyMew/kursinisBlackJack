using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class Card
    {
        private string rank;
        private string suit;

        private Card(string rank, string suit) 
        {
            this.rank = rank;
            this.suit = suit;
        }

        public int getValue()
        {

            if (rank == "Ace" || rank == "Jack" || rank == "King" || rank == "Queen")
            {
                if(rank == "Ace")
                {
                    return 11;
                }
                else
                {
                    return 10;
                }
            }
            return int.Parse(rank);

        }

        public static Card CreateCard(string rank, string suit)
        {
            return new Card(rank, suit);
        }

        public bool isAce()
        {
            if (rank == "Ace")
                return true;
            else
                return false;
        }


        public override string ToString()
        {
            return rank + " of " + suit;
        }
    }
}
