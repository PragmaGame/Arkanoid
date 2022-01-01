using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewEnterNick : MonoBehaviour
{
    [SerializeField] private ViewMenuUI _viewMenuUI;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Text _nameText;
    [SerializeField] private PlayerScoreModel _playerScoreModel;

    private void Awake()
    {
        _playerScoreModel = FindObjectOfType<PlayerScoreModel>();
    }
    
    private void OnEnable()
    {
        _saveButton.onClick.AddListener(OnClickSaveButton);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(OnClickSaveButton);
    }

    private void OnClickSaveButton()
    {
        _playerScoreModel.EnterNick(_nameText.text);
        _viewMenuUI.ActivePanel(false,true,false);
    }
}