using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemiesWaveVolume : BaseInteractionVolume
{
    [SerializeField]
    private EnemiesWaveData[] _waveData;
    [SerializeField]
    private float _limitDistance = 8.0f;
    [SerializeField]
    private float _spawnDistance = 5.0f;
    [SerializeField]
    private bool _finalWave = false;

    private float LeftLimit { get { return transform.position.z - _limitDistance; } }
    private float RightLimit { get { return transform.position.z + _limitDistance; } }

    private int _index = 0;

    protected override void OnInteraction()
    {
        EnemiesWaveManager.Instance._onAllEnemiesDead += OnAllEnemiesDead;
        SpawnNextWave();

        BordersNavigationManager.Instance.SetHorizontalLimits
        (
            LeftLimit, RightLimit
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
            BordersNavigationManager.Instance.SetHorizontalLimits(transform.position.z - _limitDistance, 99999.0f);

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
        int spawnSide = _waveData[_index]._spawnSide;
        Vector3 position = transform.position + new Vector3(0.0f, 0.0f, _spawnDistance * spawnSide);
        EnemiesWaveManager.Instance.SpawnWave(_waveData[_index], position);
        _index++;
    }

    private void OnDrawGizmosSelected()
    {
        if (_waveData.Length > 0)
        {
            foreach (EnemiesWaveData item in _waveData)
            {
                Vector3 position = transform.position + new Vector3(0.0f, 0.0f, _spawnDistance * item._spawnSide);
                Gizmos.DrawWireSphere(position, 3.0f);
            }
        }

        Vector3 left = new Vector3(4.0f, 2.0f, LeftLimit);
        Vector3 right = new Vector3(-4.0f, 2.0f, LeftLimit);
        Gizmos.DrawLine(left, right);

        left = new Vector3(4.0f, 2.0f, RightLimit);
        right = new Vector3(-4.0f, 2.0f, RightLimit);
        Gizmos.DrawLine(left, right); ;
    }
}
