using UnityEngine;

public class EnemiesLevel : MonoBehaviour
{
    [SerializeField]
    private int _increaseEnemiesCountPeriod = 4;
    [SerializeField]
    private int _increaseEnemiesHPPeriod = 3;

    private int _enemiesPerWave = 2;
    private int _enemiesHP = 1;
    private int _waveIndex = 0;

    public int GetEnemiesPerWave()
    {
        int result = Random.Range(_enemiesPerWave, _enemiesPerWave + 2);
        if (GameplayManager.Instance.GetPlayersCount() == 1)
        {
            result /= 2;
        }

        return result;
    }

    public int GetEnemiesHP()
    {
        int result = Random.Range(_enemiesHP, _enemiesHP + 2);
        if ((GameplayManager.Instance.GetPlayersCount() == 1) && (_enemiesHP > 1))
        {
            result /= 2;
        }

        return result;
    }

    public void UpdateIndex()
    {
        PrintInfo();

        _waveIndex++;

        if ((_waveIndex % _increaseEnemiesCountPeriod) == 0)
        {
            _enemiesPerWave++;
        }

        if ((_waveIndex % _increaseEnemiesHPPeriod) == 0)
        {
            _enemiesHP++;
        }
    }

    private void PrintInfo()
    {
        Debug.Log("Index: " + _waveIndex);
        Debug.Log("Enemies Per Wave: " + _enemiesPerWave);
        Debug.Log("HP: " + _enemiesHP);
        Debug.Log("=============");
    }
}
