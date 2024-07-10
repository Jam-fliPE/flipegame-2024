using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;

    private AnimationController _animationController;
    int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animationController = GetComponent<AnimationController>();
    }

    public void TakeDamage(Transform enemyTransform, int damage)
    {
        transform.LookAt(enemyTransform.position);

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animationController.PlayHitReaction();
        }
    }

    protected abstract void OnDie();

    private void Die()
    {
        _animationController.PlayDeath();
        OnDie();
    }
}
