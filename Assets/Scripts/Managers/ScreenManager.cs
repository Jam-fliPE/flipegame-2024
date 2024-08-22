using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _victoryScreen;
    [SerializeField]
    private GameObject _defeatScreen;

    public static ScreenManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuQuit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void LoadVictoryScreen()
    {
        Instantiate(_victoryScreen);
    }

    public void LoadDefeatScreen()
    {
        Instantiate(_defeatScreen);
    }
}
