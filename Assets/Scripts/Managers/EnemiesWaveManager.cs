using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AllEnemiesDeadDelegate();

public class EnemiesWaveManager : MonoBehaviour
{
    [SerializeField]
    float _spawnRadius = 3.0f;

    public static EnemiesWaveManager Instance { get; private set; }
    public event AllEnemiesDeadDelegate _onAllEnemiesDead;

    private List<AIController> _enemies;
    private WaitForSeconds _engagementDelay;

    private void Awake()
    {
        Instance = this;
        _enemies = new List<AIController>();
        _engagementDelay = new WaitForSeconds(3.0f);
    }

    public void SpawnWave(EnemiesWaveData waveData)
    {
        foreach (GameObject item in waveData._enemiesPrefab)
        {
            Vector3 position = GetRandomSpawnPoint(waveData._spawnReference.position);
            GameObject enemy = Instantiate(item, position, waveData._spawnReference.rotation);
            _enemies.Add(enemy.GetComponent<AIController>());
        }

        StartCoroutine(RefreshEngagement());
    }

    public void OnEnemyDead(AIController enemy)
    {
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

    private Vector3 GetRandomSpawnPoint(Vector3 referencePosition)
    {
        Vector3 result = referencePosition;
        result.z = result.z + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);
        result.x = result.x + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);

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
            SetClosestEnemyEngaged();
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
}
