using System.Collections;
using UnityEngine;

public class EnemiesWaveVolume : BaseInteractionVolume
{
    [SerializeField]
    private int[] _spawnSide;
    [SerializeField]
    private float _limitDistance = 8.0f;
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
        if (_index < _spawnSide.Length)
        {
            StartCoroutine(WaitAndSpawnNextWave());
        }
        else
        {
            EnemiesWaveManager.Instance._onAllEnemiesDead -= OnAllEnemiesDead;
            BordersNavigationManager.Instance.SetHorizontalLimits(transform.position.z - _limitDistance, 99999.0f);

            _index = 0;

            if (_finalWave )
            {
                ScreenManager.Instance.LoadVictoryScreen();
            }
        }
    }

    private IEnumerator WaitAndSpawnNextWave()
    {
        yield return new WaitForSeconds(1.0f);
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        int spawnSide = _spawnSide[_index];
        EnemiesWaveManager.Instance.SpawnWave(spawnSide);
        _index++;
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnSide.Length > 0)
        {
            foreach (int item in _spawnSide)
            {
                Vector3 Top = transform.position + new Vector3(-5.0f, 0.0f, 8.0f * item);
                Vector3 Bottom = transform.position + new Vector3(5.0f, 0.0f, 3.0f * item);
                Gizmos.DrawLine(Bottom, Top);
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
