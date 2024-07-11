using System.Collections;
using UnityEngine;

public delegate void AllEnemiesDeadDelegate();

public class EnemiesWaveManager : MonoBehaviour
{
    [SerializeField]
    float _spawnRadius = 3.0f;

    private ArrayList _enemies;

    public static EnemiesWaveManager Instance { get; private set; }

    public event AllEnemiesDeadDelegate _onAllEnemiesDead;

    private void Awake()
    {
        Instance = this;
        _enemies = new ArrayList();
    }

    public void SpawnWave(EnemiesWaveData waveData)
    {
        foreach (GameObject item in waveData._enemiesPrefab)
        {
            Vector3 position = GetRandomSpawnPoint(waveData._spawnReference.position);
            GameObject enemy = Instantiate(item, position, waveData._spawnReference.rotation);
            _enemies.Add(enemy.GetComponent<AIController>());
        }
    }

    public void OnEnemyDead(AIController enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            _onAllEnemiesDead?.Invoke();
        }
    }

    private Vector3 GetRandomSpawnPoint(Vector3 referencePosition)
    {
        Vector3 result = referencePosition;
        result.z = result.z + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);
        result.x = result.x + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius);

        return result;
    }
}
