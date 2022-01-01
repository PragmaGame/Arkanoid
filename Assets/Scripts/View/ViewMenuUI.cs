using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _viewScorePanel;
    [SerializeField] private GameObject _viewMenuTitlePanel;
    [SerializeField] private GameObject _viewEnterNickPanel;

    private void Start()
    {
        if (SceneManager.Instance.IsFirstGame)
        {
            ActivePanel(true,false,false);
        }
        else
        {
            ActivePanel(false,true,false);
        }
    }

    public void ActivePanel(bool enterNick, bool titleMenu, bool score)
    {
        _viewEnterNickPanel.SetActive(enterNick);
        _viewMenuTitlePanel.SetActive(titleMenu);
        _viewScorePanel.SetActive(score);
    }
}
