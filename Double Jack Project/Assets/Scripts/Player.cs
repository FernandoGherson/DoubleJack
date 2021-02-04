using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{ 
    

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Dice =" + Randomizer.UsingDice().ToString());
        }    
    }
}
