using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    
    public static SceneManager Instance => _instance;

    private AudioSource _audioSource;

    private bool _musicOn;
    private bool _isFirstGame;

    public bool IsFirstGame => _isFirstGame;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        
        _audioSource = GetComponent<AudioSource>();
        
        
        
        if(PlayerPrefs.GetInt("FIRST_GAME", 0) == 0)
        {
            //PlayerPrefs.SetInt("FIRST_GAME", 1);
            _isFirstGame = true;
        }
        else
        {
            _isFirstGame = false;
        }
    }

    private void Start()
    {
        _musicOn = PlayerPrefs.GetInt("isAudioOn") == 1;

        if (!_musicOn)
            _audioSource.Stop();
        else
            _audioSource.Play();
    }
    
    public void Audio()
    {
        if (_musicOn)
        {
            _musicOn = false;
            _audioSource.Stop();
        }
        else
        {
            _musicOn = true;
            _audioSource.Play();
        }
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("isAudioOn", _musicOn ? 1 : 0);
    }
}
