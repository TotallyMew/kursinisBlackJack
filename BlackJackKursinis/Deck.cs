﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackKursinis
{
    internal class Deck
    {

        List<Card> deck;
        Random rng = new Random();

        public Deck()
        {
            createDeck();
            shuffleDeck();
        }

        public void createDeck()
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
                        Card card = new Card(ranks[i], suits[j]);
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


        public Card drawCard()
        {
            Card lastCard = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return lastCard;
        }


    }
}