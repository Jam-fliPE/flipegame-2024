using System;
using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;
    [SerializeField]
    private LayerMask _enemyLayers;

    private AnimationController _animationController;

    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
        // _attackCollider.enabled = false;
    }

    public void LightAttack(Action callback)
    {
        StartCoroutine(_animationController.PlayLightAttack(callback));
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider item in hitEnemies)
        {
            item.GetComponent<HealthController>().TakeDamage(20);
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
