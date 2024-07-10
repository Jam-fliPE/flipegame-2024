using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    public static GameplayManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    public GameObject GetPlayer()
    {
        return _player;
    }
}
