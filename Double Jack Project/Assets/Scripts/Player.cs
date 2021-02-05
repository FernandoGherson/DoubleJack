using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{ 
    

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            for (int i = 0; i < 13; i++)
            {
                Card card = new Card(i, CardsSuits.Clubs);
                Debug.Log(card.Name);
            }
        }    
    }
}
