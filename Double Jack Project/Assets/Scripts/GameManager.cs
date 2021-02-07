using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TurnHandler _turnHandler;
    DeckOfCards _deck;
    Player[] _players;
    int _startIndex;
    void Start()
    {
        _deck = new DeckOfCards(4);
        _turnHandler = FindObjectOfType<TurnHandler>();
        TurnHandler.OnWinTurn += OnWinTurn;
        
        //fazer destruir outros iguas 

        _players = FindObjectsOfType<Player>();

        StartCoroutine(LateStart());
        _startIndex = UnityEngine.Random.Range(0, _players.Length);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        _turnHandler.StartNewTurn(_deck.DrawCard(), _players, _startIndex);
    }

    void OnWinTurn(Player winner)
    {
        Debug.Log(winner.myName + " win the turn");
        int winnerIndex = 0;
        for (int i = 1; i < _players.Length; i++)
        {
            if (_players[i] == winner)
                winnerIndex = i;
        }
        if (winnerIndex + 1 < _players.Length)
            _startIndex =  winnerIndex + 1;
        else
            _startIndex = 0;

        Debug.Log("----------------------------");
        _turnHandler.StartNewTurn(_deck.DrawCard(), _players, _startIndex);
    }
}
