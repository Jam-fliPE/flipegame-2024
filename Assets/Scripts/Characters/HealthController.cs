using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;

    private AnimationController _animationController;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animationController = GetComponent<AnimationController>();
    }

    public void TakeDamage(Transform opponentTransform, int damage)
    {
        if (IsAlive())
        {
            transform.LookAt(opponentTransform.position, Vector3.up);

            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
            else
            {
                _animationController.PlayHitReaction();
                OnTakeDamage(opponentTransform);
            }
        }
    }

    public bool IsAlive()
    {
        return (_currentHealth > 0);
    }

    protected virtual void OnTakeDamage(Transform opponentTransform) { }
    protected abstract void OnDie();

    private void Die()
    {
        _animationController.PlayDeath();
        OnDie();
    }
}
