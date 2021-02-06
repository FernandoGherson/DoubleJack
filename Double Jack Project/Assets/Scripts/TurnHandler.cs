using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class TurnHandler : MonoBehaviour
{
    public event Action<Player, int> OnNextPLayerTurn;
    public event Action<Player> OnWinTurn;

    private IEnumerator turn;
    Player[] _players;
    bool _playerFinished;
    int _startIndex;
    Card _openedCard;
    DeckOfCards _deck;
    
    
    
    void Start()
    {
        // fazer destruir outros iguas 

        _startIndex = UnityEngine.Random.Range(0, _players.Length);

        _players = FindObjectsOfType<Player>();

        if (_players.Length == 2)
            _deck = new DeckOfCards(false);
        else if(_players.Length == 4)
            _deck = new DeckOfCards(true);
        else
            //erro

        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].OnPlayerFinished += OnPlayerFinished;
        }
    }

    public void StartNewTurn()
    {
        if (turn != null)
            StopCoroutine(turn);
        turn = TurnRotine();
        StartCoroutine(turn);
    }
    
    public void StopTurn()
    {
        StopCoroutine(turn);
    }

    void OnPlayerFinished(Player p)
    {
        _playerFinished = true;
    }

    IEnumerator TurnRotine()
    {
        _openedCard = _deck.DrawCard();

        List<int> playersOnTurnPoints = new List<int>();
        int numberOfDices = _players.Length;

        for (int k = 0; k < _players.Length; k++)
        {
            int correctedIndex;
            if (_startIndex + k >= _players.Length)
                correctedIndex = _players.Length - (_startIndex + k);
            else
                correctedIndex = _startIndex + k;
            Player p = _players[correctedIndex];

            OnNextPLayerTurn?.Invoke(p, numberOfDices - k);
            _playerFinished = false;
            while (_playerFinished == false)
                yield return new WaitForSeconds(0.1f);

            if (correctedIndex == _startIndex && DetermineIfAutoWin(p))
            {
                OnWinTurn?.Invoke(p);
                yield break;
            }

            playersOnTurnPoints.Add(DeterminePlayerPoints(p));

            //coloca resultado na tela de pontos na tela ou mandar resultado de pontos para o jogador  
        }
        int victoryIndex = DetermineWiner(playersOnTurnPoints);
        OnWinTurn?.Invoke(_players[victoryIndex]);

        if (victoryIndex + 1 < _players.Length)
            _startIndex = victoryIndex + 1;
        else
            _startIndex = 0;
    }

    #region Functions for TurnRotine()
    bool DetermineIfAutoWin(Player p)
    {
        //soma igual
        int sumDices = 0;
        foreach (int n in p.dices)
            sumDices += n;

        if (sumDices == _openedCard.Value)
            return true;
        else
            return false;

        //falta 6,6, carta J
    }

    int DeterminePlayerPoints(Player p)
    {
        List<int> dicePoints = new List<int>();
        
        for (int i = 0; i < p.dices.Length; i++)
        {
            int result;

            if (_openedCard.Value % p.dices[i] == 0)
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
    #endregion
}