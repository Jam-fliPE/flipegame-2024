using System.Collections.Generic;
using UnityEngine;

public class BordersNavigationManager : MonoBehaviour
{
    [SerializeField]
    private float[] _verticalLimits = { -4.0f, 4.0f };

    [SerializeField]
    private float[] _horizontalLimits = { -5.0f, 20.0f };
    private List<Transform> _players = new List<Transform>();
    private List<Transform> _enemies = new List<Transform>();

    public static BordersNavigationManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemiesWaveManager.Instance._onEnemySpawn += OnEnemySpawn;
        EnemiesWaveManager.Instance._onEnemyDead -= OnEnemyDead;
    }

    public void AddPlayer(Transform item)
    {
        _players.Add(item);
    }

    public void AddEnemy(Transform item)
    {
        _enemies.Add(item);
    }

    public void RemoveEnemy(Transform item)
    {
        _enemies.Remove(item);
    }

    public void SetHorizontalLimits(float left, float right)
    {
        _horizontalLimits[0] = left;
        _horizontalLimits[1] = right;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            Transform item = _players[i];
            CheckHorizontalLimits(item);
            CheckVerticalLimits(item);
        }

        for (int i = 0; i < _enemies.Count; i++)
        {
            Transform item = _enemies[i];
            CheckHorizontalLimits(item);
        }
    }

    private void CheckHorizontalLimits(Transform item)
    {
        Vector3 position = item.position;
        bool needsUpdate = false;
        if (position.z < _horizontalLimits[0])
        {
            position.z = _horizontalLimits[0];
            needsUpdate = true;
        }
        else if (position.z > _horizontalLimits[1])
        {
            position.z = _horizontalLimits[1];
            needsUpdate = true;
        }

        if (needsUpdate)
        {
            item.position = position;
        }
    }

    private void CheckVerticalLimits(Transform item)
    {
        Vector3 position = item.position;
        bool needsUpdate = false;
        if (position.x < _verticalLimits[0])
        {
            position.x = _verticalLimits[0];
            needsUpdate = true;
        }
        else if (position.x > _verticalLimits[1])
        {
            position.x = _verticalLimits[1];
            needsUpdate = true;
        }

        if (needsUpdate)
        {
            item.position = position;
        }
    }

    private void OnEnemySpawn(Transform transform)
    {
        _enemies.Remove(transform);
    }

    private void OnEnemyDead(Transform transform)
    {
        _enemies.Remove(transform);
    }

    private void OnDrawGizmosSelected()
    {
        if (_verticalLimits.Length == 2)
        {
            Vector3 left = new Vector3(_verticalLimits[0], 2.0f, -20.0f);
            Vector3 right = new Vector3(_verticalLimits[0], 2.0f, 500.0f);
            Gizmos.DrawLine(left, right);

            left = new Vector3(_verticalLimits[1], 2.0f, -20.0f);
            right = new Vector3(_verticalLimits[1], 2.0f, 500.0f);
            Gizmos.DrawLine(left, right);
        }
    }
}
