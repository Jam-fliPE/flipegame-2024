using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnPlayerInstantiated(Transform playerTransform);
public delegate void OnPlayerDeath(Transform playerTransform);

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
    public OnPlayerDeath _onPlayerDeath;

    private List<Transform> _players;
    private int _playerIndex;
    private int _playersInScoreState = 0;

    private void Awake()
    {
        Instance = this;
        _playerIndex = 0;
        _players = new List<Transform>(2);
    }

    private void Start()
    {
        SoundManager.Instance.PlayGameplayBgm();
    }

    public Transform GetPlayer()
    {
        Transform result = null;
        if (_players.Count > 0)
        {
            result = _players[_playerIndex];
            _playerIndex++;
            if (_playerIndex >= _players.Count)
            {
                _playerIndex = 0;
            }
        }

        return result;
    }

    public Transform InstantiatePlayer(int index)
    {
        Transform result;

        Vector3 position = _cameraTransform.position;
        position.y = 0.0f;
        position.x = -1.0f;

        GameObject playerPrefab = _playerPrefab1;
        if (index == 0)
        {
            playerPrefab = _playerPrefab0;
            position.x = 1.0f;
        }

        result = Instantiate(playerPrefab, position, Quaternion.identity).transform;
        _players.Add(result);

        if (_players.Count == 2)
        {
            DisableJoinInput();
        }

        _onPlayerInstantiated?.Invoke(result.transform);

        return result;
    }

    public Transform GetCameraTransform()
    {
        return _cameraTransform;
    }

    public int GetPlayersCount()
    {
        return _players.Count;
    }

    public void OnPlayerDeath(Transform deadPlayer)
    {
        _playerIndex = 0;
        _players.Remove(deadPlayer);
        _onPlayerDeath?.Invoke(deadPlayer);
        if (_players.Count == 0 )
        {
            Invoke("LoadDefeat", 3.0f);
        }
        else
        {
            Destroy(deadPlayer.gameObject, 3.0f);
        }
    }

    public void OnPlayerScoreInputBegin()
    {
        _playersInScoreState++;
    }

    public void OnPlayerScoreInputEnd()
    {
        _playersInScoreState--;

        if ((_players.Count == 0) && (_playersInScoreState == 0))
        {
            Invoke("LoadLeaderboard", 1.0f);
        }
    }

    private void DisableJoinInput()
    {
        PlayerInputManager inputManager = FindObjectOfType<PlayerInputManager>();
        inputManager.enabled = false;
    }

    private void LoadDefeat()
    {
        ScreenManager.Instance.LoadDefeatScreen();
    }

    private void LoadLeaderboard()
    {
        ScreenManager.Instance.LoadLeaderboard();
    }
}
