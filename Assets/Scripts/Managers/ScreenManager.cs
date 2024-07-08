using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void MainMenuPlay()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void MainMenuQuit()
    {
        Application.Quit();
    }
}
