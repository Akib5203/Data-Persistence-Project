using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_applicationIsQuiting) return null;

            if (_instance == null) _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject gameManager = new("Game Manager", typeof(GameManager));
                DontDestroyOnLoad(gameManager);
                _instance = gameManager.GetComponent<GameManager>();
            }
            return _instance;
        }
    }

    private static bool _applicationIsQuiting = false;

    private readonly string _highScorerPrefName = "HighScorer";
    private readonly string _highScorePrefName = "HighScore";

    public string CurrentPlayerName;
    public int HighScore;
    public string HighScorer;

    public System.Action GameManagerReady;

    // This method runs whenever the Scene is loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetQuitFlag() => _applicationIsQuiting = false; // Set application is quitting flag to false, because application is running

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(_highScorerPrefName))
        {
            HighScore = 0;
            HighScorer = string.Empty;
        }
        else
        {
            HighScore = PlayerPrefs.GetInt(_highScorePrefName);
            HighScorer = PlayerPrefs.GetString(_highScorerPrefName);
        }

        CurrentPlayerName = string.Empty;

        GameManagerReady.Invoke();
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetString(_highScorerPrefName, HighScorer);
        PlayerPrefs.SetInt(_highScorePrefName, HighScore);
        PlayerPrefs.Save();
    }

    public void StartGame(string currentPlayerName)
    {
        if (string.IsNullOrEmpty(currentPlayerName)) return;

        CurrentPlayerName = currentPlayerName;
        SceneManager.LoadScene(1);
    }

    public void RefreshScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void QuitGame() => Application.Quit();

    public void InitialiseGameManager() => Debug.Log("Game Manager Initialized!");

    private void OnDestroy() => _applicationIsQuiting = true; // On Gameobject Destroyed set application is quitting flag to true
}
