using UnityEngine;
using UnityEngine.UI;

public class ViewGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _restartTitle;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private PlayerScoreModel _playerScoreModel;
    
    private void Start()
    {
        _scoreText.text = "0";
        _bestScoreText.text = _playerScoreModel.BestScore.ToString();
    }
    
    private void Awake()
    {
        _playerScoreModel = FindObjectOfType<PlayerScoreModel>();
    }
    
    private void OnEnable()
    {
        _playerScoreModel.ChangedScoreEvent += OnChangedScore;
        _playerScoreModel.ChangedBestScoreEvent += OnChangedBestScore;
        gameLogic.GameOveredEvent += OnGameOver;
    }

    private void OnDisable()
    {
        _playerScoreModel.ChangedScoreEvent -= OnChangedScore;
        _playerScoreModel.ChangedBestScoreEvent -= OnChangedBestScore;
        gameLogic.GameOveredEvent += OnGameOver;
    }

    private void OnChangedScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void OnChangedBestScore(int score)
    {
        _bestScoreText.text = score.ToString();
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
