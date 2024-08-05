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
    [SerializeField]
    private int _damage = 1;

    private AnimationController _animationController;

    protected virtual void Start()
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

            bool killed;
            healthController.TakeDamage(transform, _damage, out killed);

            if (killed)
            {
                OnKilledEnemy();
            }

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

    protected virtual void OnKilledEnemy() { }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}
