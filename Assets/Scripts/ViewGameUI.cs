using UnityEngine;
using UnityEngine.UI;

public class ViewGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _restartTitle;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameLogic gameLogic;
    
    private void Start()
    {
        _scoreText.text = "0";
    }
    
    private void OnEnable()
    {
        gameLogic.ChangeScoreEvent += OnChangedScore;
        gameLogic.GameOveredEvent += OnGameOver;
    }

    private void OnDisable()
    {
        gameLogic.ChangeScoreEvent -= OnChangedScore;
        gameLogic.GameOveredEvent += OnGameOver;
    }

    private void OnChangedScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnGameOver()
    {
        _restartTitle.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.Instance.Play();
    }
}
