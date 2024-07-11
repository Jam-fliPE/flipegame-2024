using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    private Renderer _renderer;

    protected override void OnDie()
    {
        Invoke("OnRemove", 2.0f);
    }

    private void OnRemove()
    {
        AIController controller = GetComponent<AIController>();
        EnemiesWaveManager.Instance.OnEnemyDead(controller);
        Destroy(gameObject);
    }
}
