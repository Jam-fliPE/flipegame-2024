using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadMainMenu", 4.0f);
    }

    private void LoadMainMenu()
    {
        ScreenManager.Instance.LoadMainMenu();
    }
}
