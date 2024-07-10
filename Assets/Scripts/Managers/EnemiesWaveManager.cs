using System.Collections;
using UnityEngine;

public class EnemiesWaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    float _spawnRadius = 3.0f;

    private ArrayList _enemies;

    public static EnemiesWaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _enemies = new ArrayList();
    }

    public void SpawnWave(Transform referenceTransform, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = GetRandomSpawnPoint(referenceTransform.position);
            GameObject enemy = Instantiate(_enemyPrefab, position, referenceTransform.rotation);
            _enemies.Add(enemy.GetComponent<AIController>());
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
