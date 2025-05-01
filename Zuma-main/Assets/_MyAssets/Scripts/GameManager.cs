using BansheeGz.BGSpline.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int _ballCount;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletLoadTime;
    [SerializeField, Range(1, 5)] private int _lifeCount;

    [Header("Windows")]
    [SerializeField] private Transform _lifeWindow;
    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _pauseWindow;

    public static GameManager Instance { get; private set; }
    public BGCcMath BgMath { get; private set; }
    public MoveBalls MoveBallsScript { get; private set; }

    public int BallCount => _ballCount;
    public float BallSpeed => _ballSpeed;
    public float BulletSpeed => _bulletSpeed;
    public int BulletLoadTime => _bulletLoadTime;

    private int _unlockedLevel;

    private void Awake()
    {
        Instance = this;
        BgMath = FindObjectOfType<BGCcMath>();
        MoveBallsScript = FindObjectOfType<MoveBalls>();

        _unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 5); // Открыт Level01 по умолчанию

        for (var i = 0; i < _lifeCount; i++)
        {
            _lifeWindow.GetChild(i).gameObject.SetActive(true);
        }

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOverWindow.activeSelf && !_winWindow.activeSelf)
        {
            PauseClick();
        }
    }

    public void PauseClick()
    {
        bool isPaused = _pauseWindow.activeSelf;
        _pauseWindow.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0;
    }

    public bool IsPaused()
    {
        return Time.timeScale == 0;
    }

    public void Win()
    {
        Time.timeScale = 0;
        _winWindow.SetActive(true);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentScene + 1;

        if (_unlockedLevel == currentScene)
        {
            _unlockedLevel = nextLevel;
            PlayerPrefs.SetInt("UnlockedLevel", _unlockedLevel);
            PlayerPrefs.Save(); // Сохранение прогресса
            Debug.Log($"[GameManager] Уровень {currentScene} пройден. Разблокирован {nextLevel}");
        }
    }

    public void Lose()
    {
        _lifeCount -= 1;
        _lifeWindow.GetChild(_lifeCount).gameObject.SetActive(false);

        if (_lifeCount <= 0)
        {
            Time.timeScale = 0;
            _gameOverWindow.SetActive(true);
        }
    }

    public void ContinueBtn_Click()
    {
        PauseClick();
    }

    public void MainMenuBtn_Click()
    {
        Debug.Log("[GameManager] Кнопка 'Меню' нажата, загружаем LoadingToMenu...");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("LoadingToMenu");
    }

    public void RetryBtn_Click()
    {
        Debug.Log($"[GameManager] Повторный запуск уровня {SceneManager.GetActiveScene().name}...");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelBtn_Click()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log($"[GameManager] Переход на уровень {nextScene}...");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(nextScene);
    }

    public Sprite GetRandomSprite()
    {
        return _sprites[Random.Range(0, _sprites.Length)];
    }
}
