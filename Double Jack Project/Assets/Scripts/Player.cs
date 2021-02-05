using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    DeckOfCards deck;

    private void Start()
    {
        deck = new DeckOfCards(true);
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Card card = deck.DrawCard();
            Debug.Log(card.Name);
        }    
    }
}
