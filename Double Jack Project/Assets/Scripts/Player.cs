using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerFinished;
    public static event Action<Player> OnDiceRoled;

    public string myName;
    [HideInInspector] public int[] dices;
    [HideInInspector] public int bestDice;

    bool _isMyTurn;
    int _diceIndex;

    void Start()
    {
        _isMyTurn = false;
        TurnHandler.OnNextPLayerTurn += BeginMyTurn;
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isMyTurn)
            {
                dices[_diceIndex] = Randomizer.UsingDice();
                _diceIndex += 1;
                OnDiceRoled?.Invoke(this);

                if (_diceIndex == dices.Length)
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
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i] = 0;
            }
            _diceIndex = 0;
        }
    }
}