using UnityEngine;

public class EnemyHealthController : HealthController
{
    protected override void OnTakeDamage(Transform opponentTransform, float hpPercentage)
    {
        GetComponent<AIController>().SetPlayerTarget(opponentTransform);
    }

    protected override void OnDie()
    {
        AIController controller = GetComponent<AIController>();
        controller.StopAllCoroutines();
        EnemiesWaveManager.Instance.OnEnemyDead(controller);
        Destroy(gameObject, 2.0f);
    }
}
