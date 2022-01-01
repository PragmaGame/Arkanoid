using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScoreModel : MonoBehaviour
{
    private List<PlayerScore> _playersScores;

    private bool _isNewPlayer;
    private PlayerScore _currentPlayerBestScore;
    private int _currentScore;

    public event Action<int> ChangedBestScoreEvent;
    public event Action<int> ChangedScoreEvent;
    
    public IReadOnlyList<PlayerScore> PlayersScores => _playersScores;
    
    public int BestScore
    {
        get => _currentPlayerBestScore.score;
        set
        {
            _currentPlayerBestScore.score = value;
            ChangedBestScoreEvent?.Invoke(value);
        }
    }
    
    private void Awake()
    {
        _playersScores = new List<PlayerScore>();
        _currentScore = 0;
    }

    private void Start()
    {
        _playersScores = DataBase.Loading().ToList();
    }

    public void AddScore(int value)
    {
        _currentScore += value;
        ChangedScoreEvent?.Invoke(_currentScore);

        if (_currentScore > BestScore)
        {
            BestScore = _currentScore;
        }
    }

    public void EnterNick(string nick)
    {
        foreach (var playerScore in _playersScores)
        {
            if (playerScore.name == nick)
            {
                _currentPlayerBestScore = playerScore;
                _isNewPlayer = false;
                return;
            }
        }

        _isNewPlayer = true;
        _currentPlayerBestScore = new PlayerScore(nick, 0);
        _playersScores.Add(_currentPlayerBestScore);
    }

    private void OnApplicationQuit()
    {
        if (_isNewPlayer)
        {
            DataBase.Insert(_currentPlayerBestScore);
        }
        else
        {
            DataBase.Replaсe(_currentPlayerBestScore);
        }
    }
}