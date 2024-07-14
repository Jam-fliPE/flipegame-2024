using System;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;
    [SerializeField]
    private LayerMask _enemyLayers;
    [SerializeField]
    private int _weaponType = 0;

    private AnimationController _animationController;

    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
    }

    public void LightAttack(Action callback = null)
    {
        StartCoroutine(_animationController.PlayLightAttack(callback));
    }

    // Called by light attack animation
    public void OnLightAttackEvent()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider item in hitEnemies)
        {
            HealthController healthController;
            if (item.CompareTag("Player"))
            {
                healthController = item.GetComponent<PlayerHealthController>();
            }
            else
            {
                healthController = item.GetComponent<EnemyHealthController>();
            }
            
            healthController.TakeDamage(transform, 20);
            if (_weaponType == 0)
            {
                SoundManager.Instance.PlayHardHit();
            }
            else
            {
                SoundManager.Instance.PlaySwordHit();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}
