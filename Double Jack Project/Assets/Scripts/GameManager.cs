using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TurnHandler turnHandler;
    
    void Start()
    {
        turnHandler = FindObjectOfType<TurnHandler>();
        TurnHandler.OnWinTurn += OnWinTurn;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        turnHandler.StartNewTurn();
    }

    void OnWinTurn(Player p)
    {
        Debug.Log(p.myName + " win the turn");
        Debug.Log("----------------------------");
        turnHandler.StartNewTurn();
    }
}
