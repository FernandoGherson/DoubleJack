using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action<Player> OnPlayerFinished;

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
                Debug.Log(myName + " d" + (_diceIndex+1).ToString() + " = " + dices[_diceIndex].ToString());
                _diceIndex += 1;

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
            _diceIndex = 0;
        }
    }
}
