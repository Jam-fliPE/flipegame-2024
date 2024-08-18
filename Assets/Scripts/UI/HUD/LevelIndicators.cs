using UnityEngine;

public class LevelIndicators : MonoBehaviour
{
    [SerializeField]
    private GameObject _goIndicator;
    [SerializeField]
    private GameObject _enemiesLeftIndicator;
    [SerializeField]
    private GameObject _enemiesRightIndicator;

    private Transform _cameraTransform;

    private GameObject _currentIndicator;

    private float _time;

    private void Start()
    {
        GameplayManager gameplayManager = GameplayManager.Instance;
        _cameraTransform = gameplayManager.GetCameraTransform();
        gameplayManager._onAllPlayersDead += OnAllPlayersDead;
        EnemiesWaveManager.Instance._onAllEnemiesDead += OnAllEnemiesDead;
        EnemiesWaveManager.Instance._onEnemySpawn += OnEnemySpawn;

        _currentIndicator = null;
    }

    private void Update()
    {
        if (_currentIndicator != null)
        {
            _time += Time.deltaTime;
            if (_time > 0.75f)
            {
                _time = 0.0f;
                _currentIndicator.SetActive(!_currentIndicator.activeSelf);

                CheckDistanceToCamera();
            }
        }
    }

    private void OnEnemySpawn(Transform enemyTransform)
    {
        _goIndicator.SetActive(false);

        float difference = enemyTransform.position.z - _cameraTransform.position.z;
        if (Mathf.Abs(difference) > 3.0f)
        {
            _currentIndicator = (difference < 0) ? _enemiesLeftIndicator : _enemiesRightIndicator;
        }
    }

    private void OnAllEnemiesDead()
    {
        _currentIndicator?.SetActive(false);
        _time = -1.0f;
        _currentIndicator = _goIndicator;
    }

    private void CheckDistanceToCamera()
    {
        float minEnemyDistance = EnemiesWaveManager.Instance.GetMinDistanceToEnemies(_cameraTransform.position.z);

        if (minEnemyDistance < 3.0f)
        {
            _currentIndicator.SetActive(false);
            _currentIndicator = null;
        }
    }

    private void OnAllPlayersDead()
    {
        GameplayManager.Instance._onAllPlayersDead -= OnAllPlayersDead;
        _currentIndicator?.SetActive(false);
        _currentIndicator = null;
    }
}
