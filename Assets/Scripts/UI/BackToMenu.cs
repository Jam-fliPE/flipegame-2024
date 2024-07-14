using UnityEngine;
using UnityEngine.InputSystem;

public class BackToMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    private void OnEnable()
    {
        _menu.SetActive(false);
    }

    public void CommandStart(InputAction.CallbackContext context)
    {
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
