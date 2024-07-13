using System.Collections;
using UnityEngine;

public class EnemiesWaveVolume : BaseInteractionVolume
{
    [SerializeField]
    private EnemiesWaveData[] _waveData;
    [SerializeField]
    private float[] _horizontalLimits;
    [SerializeField]
    private bool _finalWave = false;

    private int _index = 0;

    protected override void OnInteraction()
    {
        EnemiesWaveManager.Instance._onAllEnemiesDead += OnAllEnemiesDead;
        SpawnNextWave();

        BordersNavigationManager.Instance.SetHorizontalLimits
        (
            _horizontalLimits[0], _horizontalLimits[1]
        );
    }

    private void OnAllEnemiesDead()
    {
        if (_index < _waveData.Length)
        {
            StartCoroutine(WaitAndSpawnNextWave());
        }
        else
        {
            EnemiesWaveManager.Instance._onAllEnemiesDead -= OnAllEnemiesDead;
            BordersNavigationManager.Instance.SetHorizontalLimits(_horizontalLimits[0], 99999.0f);

            if (_finalWave )
            {
                ScreenManager.Instance.LoadVictoryScreen();
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitAndSpawnNextWave()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnNextWave();
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

        if (_horizontalLimits.Length == 2)
        {
            Vector3 left = new Vector3(4.0f, 2.0f, _horizontalLimits[0]);
            Vector3 right = new Vector3(-4.0f, 2.0f, _horizontalLimits[0]);
            Gizmos.DrawLine(left, right);

            left = new Vector3(4.0f, 2.0f, _horizontalLimits[1]);
            right = new Vector3(-4.0f, 2.0f, _horizontalLimits[1]);
            Gizmos.DrawLine(left, right); ;
        }
    }
}
