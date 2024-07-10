using UnityEngine;

public class EnemiesWaveVolume : BaseInteractionVolume
{
    [SerializeField]
    private Transform _spawnReference;
    [SerializeField]
    private int _enemiesCount = 1;

    protected override void OnInteraction()
    {
        EnemiesWaveManager.Instance.SpawnWave(_spawnReference, _enemiesCount);
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnReference != null)
        {
            Gizmos.DrawWireSphere(_spawnReference.position, 3.0f);
        }
    }
}
