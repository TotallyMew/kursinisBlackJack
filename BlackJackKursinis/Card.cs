using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Card
    {
        string rank;
        string suit;

        public Card(string rank, string suit) 
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
