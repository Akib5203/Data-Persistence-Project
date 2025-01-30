using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _scoreValue;
    [SerializeField] private TMPro.TMP_InputField _inputName;

    [SerializeField] private UnityEngine.UI.Button _startButton;
    [SerializeField] private UnityEngine.UI.Button _quitButton;

    private void Awake() => GameManager.Instance.InitialiseGameManager();

    private void OnEnable()
    {
        GameManager.Instance.GameManagerReady += OnGameManagerReady;

        _startButton.onClick.AddListener(OnStartButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnGameManagerReady() => _scoreValue.text = GameManager.Instance.HighScorer + ": " + GameManager.Instance.HighScore;

    private void OnStartButtonClicked() => GameManager.Instance.StartGame(_inputName.text);

    private void OnQuitButtonClicked() => GameManager.Instance.QuitGame();

    private void OnDisable()
    {
        if (GameManager.Instance != null) GameManager.Instance.GameManagerReady -= OnGameManagerReady;

        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }
}
