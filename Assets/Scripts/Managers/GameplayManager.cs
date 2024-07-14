using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;

    public static GameplayManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SoundManager.Instance.PlayGameplayBgm();
    }

    public GameObject GetPlayer()
    {
        int index = Random.Range(0, 1);
        if (index == 0)
        {
            return _player1;
        }

        return _player2;
    }
}
