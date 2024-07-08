using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private static GameplayManager _instance = null;

    private void Awake()
    {
        _instance = this;
    }

    public static GameplayManager GetInstance()
    {
        return _instance;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
