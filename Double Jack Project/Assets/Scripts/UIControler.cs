using UnityEngine;
using TMPro;

public class UIControler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] TextMeshProUGUI _playerDiceA;
    [SerializeField] TextMeshProUGUI _playerDiceB;
    [SerializeField] TextMeshProUGUI _aiName;
    [SerializeField] TextMeshProUGUI _aiDiceA;
    [SerializeField] TextMeshProUGUI _aiDiceB;
    [SerializeField] TextMeshProUGUI _card;

    int _playerPlaying;
    int _startIndex;

    private void Start()
    {
        Player.OnPlayerFinished += OnPlayerFinished;
        Player.OnDiceRoled += OnDiceRoled;
        _playerName.text = GameManager.PlayersArray[0].myName;
        _aiName.text = GameManager.PlayersArray[1].myName;
        _card.text = " ";
        _playerDiceA.text = " ";
        _playerDiceB.text = " ";
        _aiDiceA.text = " ";
        _aiDiceB.text = " ";
    }

    void OnDiceRoled(Player p)
    {
        if (_playerPlaying == _startIndex)
        {
            if (_playerPlaying == 0)
            {
                if (p.dices[0] != 0)
                    _playerDiceA.text = p.dices[0].ToString();
                if (p.dices[p.dices.Length - 1] != 0)
                    _playerDiceB.text = p.dices[p.dices.Length - 1].ToString();
            }
            if (_playerPlaying == 1)
            {
                if (p.dices[0] != 0)
                    _aiDiceA.text = p.dices[0].ToString();
                if (p.dices[p.dices.Length - 1] != 0)
                    _aiDiceB.text = p.dices[p.dices.Length - 1].ToString();
            }
        }
        else
        {
            if (_playerPlaying == 0)
            {
                if (p.dices[0] != 0)
                    _playerDiceA.text = p.dices[0].ToString();
            }
            if (_playerPlaying == 1)
            {
                if (p.dices[0] != 0)
                    _aiDiceA.text = p.dices[0].ToString();
            }
        }
    }

    void OnPlayerFinished(Player p)
    {
        if (p == GameManager.PlayersArray[_playerPlaying])
            if (_playerPlaying + 1 < GameManager.PlayersArray.Length)
                _playerPlaying++;
            else
                _playerPlaying = 0;
    }

    public void OnStartNewTurn(int st, Card cd)
    {
        _startIndex = st;
        _playerPlaying = _startIndex;
        _card.text = cd.Name.ToString();
        _playerDiceA.text = " ";
        _playerDiceB.text = " ";
        _aiDiceA.text = " ";
        _aiDiceB.text = " ";
    }
}

