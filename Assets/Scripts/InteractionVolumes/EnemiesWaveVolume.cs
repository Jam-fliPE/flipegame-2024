using UnityEngine;

public class EnemiesWaveVolume : BaseInteractionVolume
{
    [SerializeField]
    private EnemiesWaveData[] _waveData;

    private int _index = 0;

    protected override void OnInteraction()
    {
        EnemiesWaveManager.Instance._onAllEnemiesDead += OnAllEnemiesDead;
        SpawnNextWave();
    }

    private void OnAllEnemiesDead()
    {
        if (_index < _waveData.Length)
        {
            SpawnNextWave();
        }
        else
        {
            EnemiesWaveManager.Instance._onAllEnemiesDead -= OnAllEnemiesDead;
            Destroy(gameObject);
        }
    }

    private void SpawnNextWave()
    {
        EnemiesWaveManager.Instance.SpawnWave(_waveData[_index]);
        _index++;
    }

    private void OnDrawGizmosSelected()
    {
        if (_waveData.Length > 0)
        {
            foreach (EnemiesWaveData item in _waveData)
            {
                if (item._spawnReference != null)
                {
                    Gizmos.DrawWireSphere(item._spawnReference.position, 3.0f);
                }
            }
        }
    }
}
