using UnityEngine;

public enum CardsSuits {Diamonds, Clubs, Hearts, Spades};

public class Card
{
    public CardsSuits Suit { get; private set; }
    public int Value { get; private set; }
    public string Name { get; private set; }
    public bool IsAJack { get; private set; }
    public bool IsAQueen { get; private set; }

    public Card(int number, CardsSuits suit)
    {
        IsAJack = false;
        IsAQueen = false;

        Suit = suit;

        if (number > 0 && number < 11) // is not a J or Q
        {
            Value = number;
            Name = Value.ToString() + " of " + Suit.ToString();
        }
        else if (number == 11) // J
        {
            Name = "Jack of " + Suit.ToString();
            IsAJack = true;
        }
        else if (number == 12) // Q
        {
            Name = "Queen of " + Suit.ToString();
            IsAQueen = true;
        }
        else
        {
            Name = number.ToString() + " is a invalid number";
            Value = 0;
            Debug.LogError("The number " + number.ToString() + " in new Card(number, suit) is invalid.\nUse a value from 1 to 12");
        }
    }
}
