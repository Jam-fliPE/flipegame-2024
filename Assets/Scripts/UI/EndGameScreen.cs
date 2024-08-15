using UnityEngine;
using UnityEngine.InputSystem;

public class EndGameScreen : MonoBehaviour
{
    public void Select(InputAction.CallbackContext context)
    {
        Invoke("LoadMainMenu", 4.0f);
    }

    private void LoadMainMenu()
    {
        ScreenManager.Instance.LoadMainMenu();
    }
}
