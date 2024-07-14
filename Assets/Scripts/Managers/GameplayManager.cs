using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;

    public static GameplayManager Instance { get; private set; }

    private GameObject[] _players;
    private int _playerIndex;

    private void Awake()
    {
        Instance = this;
        _playerIndex = 0;
        _players = new GameObject[2] { _player1, _player2 };
    }

    private void Start()
    {
        SoundManager.Instance.PlayGameplayBgm();
    }

    public GameObject GetPlayer()
    {
        GameObject result = _players[_playerIndex];
        _playerIndex++;
        if (_playerIndex >= _players.Length)
        {
            _playerIndex = 0;
        }

        return result;
    }
}
