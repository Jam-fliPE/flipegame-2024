using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField]
    private Renderer _renderer;

    protected override void OnTakeDamage(Transform opponentTransform)
    {
        GetComponent<AIController>().PlayerTransform = opponentTransform;
    }

    protected override void OnDie()
    {
        AIController controller = GetComponent<AIController>();
        controller.StopAllCoroutines();
        EnemiesWaveManager.Instance.OnEnemyDead(controller);
        Destroy(gameObject, 2.0f);
    }
}
