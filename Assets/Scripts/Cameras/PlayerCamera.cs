using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private List<Transform> _players;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        _players = new List<Transform>(2);
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
    }

    void LateUpdate()
    {
        if (_players.Count > 0)
        {
            Vector3 position = _transform.position;
            float zPosition = 0.0f;

            for (int i = 0; i < _players.Count; i++)
            {
                zPosition += _players[i].position.z;
            }

            position.z = zPosition / _players.Count;

            _transform.position = position;
        }
    }

    private void OnPlayerInstantiated(Transform playerTransform)
    {
        _players.Add(playerTransform);
    }
}
