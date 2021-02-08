using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    TurnHandler _turnHandler;
    DeckOfCards _deck;
    Player[] _playersArray;
    int _startIndex;

    void Awake()
    {
        if (Instance == null)        
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        _deck = new DeckOfCards(4);
        _turnHandler = FindObjectOfType<TurnHandler>();
        TurnHandler.OnWinTurn += OnWinTurn;
        
        //fazer destruir outros iguas 

        _playersArray = FindObjectsOfType<Player>();

        StartCoroutine(LateStart());
        _startIndex = UnityEngine.Random.Range(0, _playersArray.Length);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        _turnHandler.StartNewTurn(_deck.DrawCard(), _playersArray, _startIndex);
    }

    void OnWinTurn(int winnerIndex)
    {       
        Debug.Log(_playersArray[winnerIndex].myName + " win the turn");

        if (winnerIndex + 1 < _playersArray.Length)
            _startIndex =  winnerIndex + 1;
        else
            _startIndex = 0;

        Debug.Log("----------------------------");
        _turnHandler.StartNewTurn(_deck.DrawCard(), _playersArray, _startIndex);
    }
}
