using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    public event Action<Player> OnPlayerFinished;

    public string playerName;
    [HideInInspector] public int[] dices;
    [HideInInspector] public int bestDice;

    bool _isMyTurn;
    int _diceIndex;

    void Start()
    {
        _isMyTurn = false;
        FindObjectOfType<TurnHandler>().OnNextPLayerTurn += BeginMyTurn;
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isMyTurn)
            {
                dices[_diceIndex] = Randomizer.UsingDice();
                _diceIndex++;

                if (_diceIndex == dices.Length - 1)
                {
                    _isMyTurn = false;
                    OnPlayerFinished?.Invoke(this);
                }
            }            
        }    
    }

    void BeginMyTurn(Player p, int numberOfDices)
    {
        if (p == this)
        {
            _isMyTurn = true;
            Array.Resize(ref dices, numberOfDices);
            _diceIndex = 0;
        }
    }
}
