using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AllEnemiesDeadDelegate();
public delegate void EnemyDeadDelegate(Transform enemyTransform);
public delegate void EnemySpawnDelegate(Transform enemyTransform);

public class EnemiesWaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnRadius = 3.0f;

    public static EnemiesWaveManager Instance { get; private set; }
    public event AllEnemiesDeadDelegate _onAllEnemiesDead;
    public event EnemyDeadDelegate _onEnemyDead;
    public event EnemySpawnDelegate _onEnemySpawn;

    private List<AIController> _enemies;
    private WaitForSeconds _engagementDelay;
    private EnemiesLevel _enemiesLevel;

    private void Awake()
    {
        Instance = this;
        _enemies = new List<AIController>();
        _engagementDelay = new WaitForSeconds(3.0f);
        _enemiesLevel = GetComponent<EnemiesLevel>();
    }

    public void SpawnWave(Vector3 referencePosition)
    {
        int enemiesCount = _enemiesLevel.GetEnemiesPerWave();
        for (int i = 0; i < enemiesCount; i++)
        {
            Vector3 position = GetRandomSpawnPoint(referencePosition);
            GameObject enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            enemy.GetComponent<HealthController>()._maxHealth = _enemiesLevel.GetEnemiesHP();
            _enemies.Add(enemy.GetComponent<AIController>());
            _onEnemySpawn?.Invoke(enemy.transform);
        }

        _enemiesLevel.UpdateIndex();
        StartCoroutine(RefreshEngagement());
    }

    public void OnEnemyDead(AIController enemy)
    {
        _onEnemyDead?.Invoke(enemy.transform);
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            _onAllEnemiesDead?.Invoke();
        }
        else
        {
            StartCoroutine(RefreshEngagement());
        }
    }

    public float GetMinDistanceToEnemies(float referencePosition)
    {
        float result = float.MaxValue;

        foreach (AIController item in _enemies)
        {
            float distance = Mathf.Abs(item.transform.position.z - referencePosition);
            if (distance < result)
            {
                result = distance;
            }
        }

        return result;
    }

    private Vector3 GetRandomSpawnPoint(Vector3 referencePosition)
    {
        Vector3 result = referencePosition;
        result.z = result.z + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);
        result.x = result.x + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);

        UpdateToSafePosition(ref result);

        return result;
    }

    private IEnumerator RefreshEngagement()
    {
        if (_enemies.Count == 1)
        {
            _enemies[0].IsEngaged = true;
            yield break;
        }

        yield return null;

        while (_enemies.Count > 1)
        {
            SetRandomEnemiesEngaged();
            yield return _engagementDelay;
        }
    }

    private void SetClosestEnemyEngaged()
    {
        float minDistance = float.MaxValue;
        AIController closestEnemy = null;
        foreach (AIController item in _enemies)
        {
            item.IsEngaged = false;

            Vector3 targetPosition = item.PlayerTransform.position;
            float distance = Vector3.Distance(targetPosition, item.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = item;
            }
        }

        if (closestEnemy != null)
        {
            closestEnemy.IsEngaged = true;
        }
    }

    private void ResetEngagement()
    {
        foreach (AIController item in _enemies)
        {
            item.IsEngaged = false;
        }
    }

    private void SetRandomEnemiesEngaged()
    {
        ResetEngagement();

        int index = Random.Range(0, _enemies.Count);
        _enemies[index].IsEngaged = true;

        if (GameplayManager.Instance.GetPlayersCount() > 1)
        {
            index++;
            if (index >= _enemies.Count)
            {
                index = 0;
            }

            _enemies[index].IsEngaged = true;
        }
    }

    private void UpdateToSafePosition(ref Vector3 position)
    {
        bool updatePosition = true;
        while (updatePosition)
        {
            updatePosition = false;
            foreach (AIController item in _enemies)
            {
                if (Vector3.Distance(item.transform.position, position) < 0.75f)
                {
                    position.z += 0.5f;
                    updatePosition = true;
                    break;
                }
            }
        }
    }
}
