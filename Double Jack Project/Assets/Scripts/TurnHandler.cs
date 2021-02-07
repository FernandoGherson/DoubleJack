using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnHandler : MonoBehaviour
{
    public static event Action<Player, int> OnNextPLayerTurn;
    public static event Action<Player> OnWinTurn;

    private IEnumerator turn;
    Player[] _players;
    bool _playerFinished;
    int _startIndex;
    Card _openedCard;    
    
    
    void Start()
    {
        // fazer destruir outros iguas 

        //_startIndex = UnityEngine.Random.Range(0, _players.Length);

        /*
        _players = FindObjectsOfType<Player>();
        
        Debug.Log("numero de jogadores = " + _players.Length.ToString());

        Debug.Log(_players[0].myName);
        Debug.Log(_players[1].myName);
        Debug.Log(_players[2].myName);
        Debug.Log(_players[3].myName);
        */

        Player.OnPlayerFinished += OnPlayerFinished;
    }

    public void StartNewTurn(Card newCard, Player[] thePlayers, int whoStart)
    {
        _openedCard = newCard;
        _players = thePlayers;
        _startIndex = whoStart;

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
        //_openedCard = _deck.DrawCard();
        Debug.Log("card is a " + _openedCard.Name);

        List<int> playersOnTurnPoints = new List<int>();
        int numberOfDices = _players.Length;

        for (int k = 0; k < _players.Length; k++)
        {
            int correctedIndex = 0;
            if (_startIndex + k == _players.Length)
            {
                correctedIndex = 0;
                _startIndex = 0;
            }
            else
                correctedIndex = _startIndex + k;
            Player p = _players[correctedIndex];

            OnNextPLayerTurn?.Invoke(p, numberOfDices - k);
            _playerFinished = false;
            while (_playerFinished == false)
                yield return new WaitForSeconds(0.1f);

            if (correctedIndex == _startIndex)
            {
                if (DetermineIfAutoWin(p))
                {
                    OnWinTurn?.Invoke(p);
                    yield break;
                }
            }

            playersOnTurnPoints.Add(DeterminePlayerPoints(p));

            //coloca resultado na tela de pontos na tela ou mandar resultado de pontos para o jogador  
        }
        int victoryIndex = DetermineWiner(playersOnTurnPoints);
        OnWinTurn?.Invoke(_players[victoryIndex]);
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

        //falta 6,6, carta Jack
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
    #endregion
}