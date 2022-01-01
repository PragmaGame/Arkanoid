using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewPlayerScore : MonoBehaviour
{
    [SerializeField] private ViewMenuUI _viewMenuUI;
    [SerializeField] private Transform _scoreContainer;
    [SerializeField] private PlayerScoreModel _playerScoreModel;

    [SerializeField] private ViewPlayerScoreCell _viewPlayerScoreCell;
    
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _playerScoreModel = FindObjectOfType<PlayerScoreModel>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnClickExitButton);

        var scores = _playerScoreModel.PlayersScores;

        foreach (var score in scores)
        {
            var cell = Instantiate(_viewPlayerScoreCell, _scoreContainer);
            cell.SetPlayerScore(score);
        }
    }
    

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnClickExitButton);

        for (var i = 0; i < _scoreContainer.childCount; i++)
        {
            Destroy(_scoreContainer.GetChild(i).gameObject);
        }
    }

    private void OnClickExitButton()
    {
        _viewMenuUI.ActivePanel(false,true, false);
    }
}