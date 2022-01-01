using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewTitleMenu : MonoBehaviour
{
    [SerializeField] private ViewMenuUI _viewMenuUI;
    
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _scoreButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnClickButtonPlay);
        _audioButton.onClick.AddListener(OnClickAudioOnOff);
        _scoreButton.onClick.AddListener(OnClickScoreButton);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnClickButtonPlay);
        _audioButton.onClick.RemoveListener(OnClickAudioOnOff);
        _scoreButton.onClick.RemoveListener(OnClickScoreButton);
    }

    private void OnClickButtonPlay()
    {
        SceneManager.Instance.Play();
    }

    private void OnClickAudioOnOff()
    {
        SceneManager.Instance.Audio();
    }

    private void OnClickScoreButton()
    {
        _viewMenuUI.ActivePanel(false,false, true);
    }
}