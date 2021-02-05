using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOfCards
{
    List<Card> _deck = new List<Card>();

    public DeckOfCards(bool doubleSizedDeck) // 2 players mode => doubleSizedDeck = false ;  4 players mode => doubleSizedDeck = true 
    {
        int maxValue = (doubleSizedDeck ? 12 : 11);

        AddBlackCards(maxValue);

        if (doubleSizedDeck)
            AddRedCards();

        Shuffle(); // Fisher-Yates shuffle
    }

    #region Constructor functions
    void AddBlackCards(int max)
    {
        // add Clubs
        for (int i = 1; i < max + 1; i++)
        {
            _deck.Add(new Card(i, CardsSuits.Clubs));
        }
        // add Spades
        for (int i = 1; i < max + 1; i++)
        {
            _deck.Add(new Card(i, CardsSuits.Spades));
        }
    }

    void AddRedCards()
    {
        // in doubleSizedDeck maxValeu = 12

        // add Diamonds
        for (int i = 1; i < 12 + 1; i++)
        {
            _deck.Add(new Card(i, CardsSuits.Diamonds));
        }
        // add Hearts
        for (int i = 1; i < 12 + 1; i++)
        {
            _deck.Add(new Card(i, CardsSuits.Hearts));
        }
    }

    void Shuffle() // Fisher-Yates shuffle
    {
        for (int i = _deck.Count-1; i > 0; i--)
        {
            int k = Random.Range(0, i);
            Card temp = _deck[i];
            _deck[i] = _deck[k];
            _deck[k] = temp;
        }
    }
    #endregion

    public Card DrawCard()
    {
        Card temp = _deck[0];
        _deck.Remove(_deck[0]);
        return temp;
    }
}


