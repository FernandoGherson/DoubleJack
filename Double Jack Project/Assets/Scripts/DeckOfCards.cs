using System.Collections.Generic;
using UnityEngine;

public class DeckOfCards
{
    List<Card> _deck = new List<Card>();

    public DeckOfCards()
    {
        AddCards();

        Shuffle(); // Fisher-Yates shuffle
    }

    #region Constructor functions
    void AddCards()
    {
        // add all balck cards
        for (int i = 2; i < 12 + 1; i++)
        {
            _deck.Add(new Card(i, CardsSuits.Clubs));
            _deck.Add(new Card(i, CardsSuits.Spades));
        }
        // add the rad jacks
        _deck.Add(new Card(11, CardsSuits.Diamonds));
        _deck.Add(new Card(11, CardsSuits.Hearts));
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


