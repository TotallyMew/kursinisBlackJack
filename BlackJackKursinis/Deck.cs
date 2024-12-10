using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    public class Deck
    {
        private static Deck singletonInstance;
        public List<Card> deck;
        readonly Random rng = new Random();
        

        private Deck()
        {
            rng = new Random();
            fillDeck();
            shuffleDeck();
        }

        public static Deck GetInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new Deck();
            }
            return singletonInstance;
        }

        public void fillDeck()
        {
            deck = new List<Card>();
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" ,"Ace" };
            string[] suits = { "Diamonds", "Hearts", "Spades", "Clubs" };

            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    for (int j = 0; j < suits.Length; j++)
                    {
                        Card card = Card.CreateCard(ranks[i], suits[j]);
                        deck.Add(card);
                    }
                }
            }

        }
        public void shuffleDeck()
        {

            int n = deck.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        

        public void AddCardToTop(Card card)
        {
            if (deck == null)
            {
                deck = new List<Card>();
            }

            deck.Add(card); // Adds the card to the top of the deck
        }

        public Card drawCard()
        {
            Card lastCard = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return lastCard;
        }


    }
}
