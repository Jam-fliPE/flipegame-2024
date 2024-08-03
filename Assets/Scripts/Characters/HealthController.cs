using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    public int _maxHealth = 1;

    private AnimationController _animationController;
    private int _currentHealth;

    public bool OnHit { get; private set; }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animationController = GetComponent<AnimationController>();
        OnHit = false;
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
                OnHit = true;
                Invoke("RestoreHit", 1.0f);
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

    private void RestoreHit()
    {
        OnHit = false;
    }
}
