using System.Collections.Generic;
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
        List<string> names = DatabaseManager.Instance.GetNames();
        List<int> scores = DatabaseManager.Instance.GetScores();
        for (int i = 0; i < 5; i++)
        {
            if (scores[i] > 0)
            {
                LeaderboardEntryView entry = Instantiate(_entryPrefab, _entryTransforms[i]).GetComponent<LeaderboardEntryView>();
                string position = string.Format("{0}#", i + 1);
                entry.Setup(position, names[i], scores[i].ToString());
            }
            else
            {
                break;
            }
        }

        Invoke("LoadMenu", 5.0f);
    }
    
    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StopAllCoroutines();
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
