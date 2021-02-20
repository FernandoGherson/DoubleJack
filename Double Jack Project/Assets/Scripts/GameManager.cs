using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Player[] PlayersArray { get; private set;}

    TurnHandler _turnHandler;
    DeckOfCards _deck;
    int _startIndex;

    IEnumerator _nextTurn;

    void Awake()
    {
        if (Instance == null)        
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        PlayersArray = FindObjectsOfType<Player>();


    }

    void Start()
    {
        _deck = new DeckOfCards();
        _turnHandler = FindObjectOfType<TurnHandler>();
        TurnHandler.OnWinTurn += OnWinTurn;
        StartCoroutine(LateStart());
        _startIndex = UnityEngine.Random.Range(0, PlayersArray.Length);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        Card theCard = _deck.DrawCard();
        _turnHandler.StartNewTurn(theCard, PlayersArray, _startIndex);
        FindObjectOfType<UIControler>().OnStartNewTurn(_startIndex, theCard);
    }

    void OnWinTurn(int winnerIndex)
    {       
        Debug.Log(PlayersArray[winnerIndex].myName + " win the turn");

        if (winnerIndex + 1 < PlayersArray.Length)
            _startIndex =  winnerIndex + 1;
        else
            _startIndex = 0;

        if (_nextTurn != null)
            StopCoroutine(_nextTurn);
        _nextTurn = NextTurn();
        StartCoroutine(_nextTurn);        
    }

    IEnumerator NextTurn()
    {
        yield return new WaitForSeconds(3);
        Card theCard = _deck.DrawCard();
        _turnHandler.StartNewTurn(theCard, PlayersArray, _startIndex);
        FindObjectOfType<UIControler>().OnStartNewTurn(_startIndex, theCard);
    }
}
