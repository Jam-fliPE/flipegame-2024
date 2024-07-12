using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    private Renderer _renderer;

    protected override void OnDie()
    {
        AIController controller = GetComponent<AIController>();
        EnemiesWaveManager.Instance.OnEnemyDead(controller);
        Destroy(gameObject, 2.0f);
    }
}
