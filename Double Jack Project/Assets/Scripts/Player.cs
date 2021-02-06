using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public event Action<Player> OnPlayerFinished;

    [HideInInspector] public int[] dices;
    [HideInInspector] public int bestDice;
    public string playerName;

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // if it is my turn
            //dice.Add(Randomizer.UsingDice());
            // termina
            OnPlayerFinished?.Invoke(this);
            //else


        }    
    }

    public void UseDices(int numberOfDices)
    {
        
    }
}
