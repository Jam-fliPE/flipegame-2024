using UnityEngine;
using UnityEngine.InputSystem;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField]
    private GameObject _entryPrefab;
    [SerializeField]
    private Transform[] _entryTransforms;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            LeaderboardEntryView entry = Instantiate(_entryPrefab, _entryTransforms[i]).GetComponent<LeaderboardEntryView>();
            entry.Setup("XYZ", "99");
        }
    }
    
    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponent<PlayerInput>().enabled = false;
            SoundManager.Instance.PlayMenuSelection();
            Invoke("LoadMenu", 1.0f);
        }
    }

    private void LoadMenu()
    {
        ScreenManager.Instance.LoadMainMenu();
    }
}
