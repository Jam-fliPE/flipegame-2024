using System.Collections.Generic;
using UnityEngine;

public delegate void OnPlayerInstantiated(Transform playerTransform);

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab0;
    [SerializeField]
    private GameObject _playerPrefab1;
    [SerializeField]
    private Transform _cameraTransform;

    public static GameplayManager Instance { get; private set; }

    public OnPlayerInstantiated _onPlayerInstantiated;

    private List<GameObject> _players;
    private int _playerIndex;

    private void Awake()
    {
        Instance = this;
        _playerIndex = 0;
        _players = new List<GameObject>(2);
    }

    private void Start()
    {
        SoundManager.Instance.PlayGameplayBgm();
    }

    public GameObject GetPlayer()
    {
        GameObject result = _players[_playerIndex];
        _playerIndex++;
        if (_playerIndex >= _players.Count)
        {
            _playerIndex = 0;
        }

        return result;
    }

    public GameObject InstantiatePlayer(int index)
    {
        GameObject result;

        Vector3 position = _cameraTransform.position;
        position.y = 0.0f;
        position.x = -1.0f;

        GameObject playerPrefab = _playerPrefab1;
        if (index == 0)
        {
            playerPrefab = _playerPrefab0;
            position.x = 1.0f;
        }

        result = Instantiate(playerPrefab, position, Quaternion.identity);
        _players.Add(result);

        _onPlayerInstantiated?.Invoke(result.transform);

        return result;
    }

    public int GetPlayersCount()
    {
        return _players.Count;
    }
}
