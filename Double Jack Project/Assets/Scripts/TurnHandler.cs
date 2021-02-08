using System;  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnHandler : MonoBehaviour
{
    public static TurnHandler Instance { get; private set; }
    public static event Action<Player, int> OnNextPLayerTurn;
    public static event Action<int> OnWinTurn;

    IEnumerator turn;
    Card _openedCard;
    Player[] _playersArray;
    int _startIndex;
    Queue<Player> _playersOrder = new Queue<Player>();
    Player _playingNow;
    bool _thePlayerFinished;

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
    
    void Start() => Player.OnPlayerFinished += OnPlayerFinished;

    #region External comands
    public void StartNewTurn(Card newCard, Player[] players, int whoStart)
    {
        _openedCard = newCard;
        _playersArray = players;
        _startIndex = whoStart;

        if (turn != null)
            StopCoroutine(turn);
        turn = TurnRotine();
        StartCoroutine(turn);
    }

    public void StopTurn() => StopCoroutine(turn);
    #endregion

    void OnPlayerFinished(Player p) => _thePlayerFinished = true;

    IEnumerator TurnRotine()
    {
        BuildQueue();

        Debug.Log("card is a " + _openedCard.Name);

        List<int> playersTurnPoints = new List<int>();

        for (int k = 0; k < _playersArray.Length; k++)
        {
            _playingNow = _playersOrder.Dequeue();
            OnNextPLayerTurn?.Invoke(_playingNow, _playersArray.Length - k);
            _thePlayerFinished = false;
            while (_thePlayerFinished == false)
                yield return new WaitForSeconds(0.1f);

            if (DetermineIfAutoWin(_playingNow) && k == 0)
            {
                SendWinner(k); 
                yield break;   
            }
            playersTurnPoints.Add(DeterminePlayerPoints(_playingNow));

            //coloca resultado na tela de pontos na tela ou mandar resultado de pontos para o jogador  
        }

        SendWinner(DetermineWiner(playersTurnPoints));
    }

    #region Functions for TurnRotine()
    void BuildQueue()
    {
        _playersOrder.Clear();
        for (int i = 0; i < _playersArray.Length; i++)
        {
            int finalIndex;
            if (_startIndex + i < _playersArray.Length)
                finalIndex = _startIndex + i;
            else
                finalIndex = _startIndex + i - _playersArray.Length;
            _playersOrder.Enqueue(_playersArray[finalIndex]);
        }
    }

    bool DetermineIfAutoWin(Player p)
    {
        int sumDices = 0;
        foreach (int n in p.dices)
            sumDices += n;
        if (sumDices == _openedCard.Value)
            return true;
        else
            return false;

        //6,6, card Jack
    }

    int DeterminePlayerPoints(Player p)
    {
        List<int> dicePoints = new List<int>();
        
        for (int i = 0; i < p.dices.Length; i++)
        {
            int result;
            if (_openedCard.Value % p.dices[i] == 0 && p.dices[i] != 1)
                result = (_openedCard.Value / p.dices[i]) * 10; 
            else if (_openedCard.Value == p.dices[i])
                result = 15;
            else
                result = _openedCard.Value - p.dices[i];
            dicePoints.Add(result);
        }

        int bestDiceIndex = 0;
        for (int i = 1; i < dicePoints.Count; i++)
        {
            if (dicePoints[i] > dicePoints[bestDiceIndex])
                bestDiceIndex = i;
        }

        p.bestDice = p.dices[bestDiceIndex];
        return dicePoints[bestDiceIndex];
    }

    int DetermineWiner(List<int> playerPoints)
    {
        int winner = 0;
        for (int i = 1; i < playerPoints.Count; i++)
        {
            if (playerPoints[i] > playerPoints[winner])
                winner = i;
        }
        return winner;
    }

    void SendWinner(int queueIndex)
    {
        int arrayIndex;
        if ((_startIndex + queueIndex) < _playersArray.Length)
            arrayIndex = _startIndex + queueIndex;
        else
            arrayIndex = _startIndex + queueIndex - _playersArray.Length;
        OnWinTurn?.Invoke(arrayIndex);
    }
    #endregion
}

