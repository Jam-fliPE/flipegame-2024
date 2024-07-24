using System.Collections.Generic;
using UnityEngine;

public class WorldCycleContainer : MonoBehaviour
{
    [SerializeField]
    private Transform[] _containers;
    [SerializeField]
    private Transform _cycleTrigger;

    private List<Transform> _players;
    private Transform _cameraTransform;

    private int _currentIndex;
    private float _secondContainerPosition;
    private void Start()
    {
        _secondContainerPosition = _containers[1].position.z;
        _currentIndex = 0;
        _players = new List<Transform>();
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
        _cameraTransform = GameplayManager.Instance.GetCameraTransform();
    }

    private void Update()
    {
        if (_cameraTransform.position.z > _cycleTrigger.position.z)
        {
            Cycle();
        }
    }

    private void OnPlayerInstantiated(Transform playerTransform)
    {
        playerTransform.SetParent(_containers[_currentIndex], true);
        _players.Add(playerTransform);
    }

    private void Cycle()
    {
        BordersNavigationManager.Instance.ResetLimits();

        Transform previousContainer = _containers[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _containers.Length;
        Transform currentContainer = _containers[_currentIndex];

        Vector3 position = Vector3.zero;
        foreach (Transform item in _players)
        {
            item.SetParent(_containers[_currentIndex], true);
        }

        position = currentContainer.position;
        position.z = 0.0f;
        currentContainer.position = position;

        position = previousContainer.position;
        position.z = _secondContainerPosition;
        previousContainer.position = position;

        previousContainer.GetComponent<RandomTransformItems>().Randomize();

        Physics.SyncTransforms();
    }
}
