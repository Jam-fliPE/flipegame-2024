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

    public void TakeDamage(Transform opponentTransform, int damage, out bool killed)
    {
        killed = false;
        if (IsAlive())
        {
            transform.LookAt(opponentTransform.position, Vector3.up);

            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                killed = true;
                Die();
            }
            else
            {
                OnHit = true;
                Invoke("RestoreHit", 1.0f);
                _animationController.PlayHitReaction();
                float hpPercentage = (float)_currentHealth / _maxHealth;
                OnTakeDamage(opponentTransform, hpPercentage);
            }
        }
    }

    public bool IsAlive()
    {
        return (_currentHealth > 0);
    }

    protected abstract void OnTakeDamage(Transform opponentTransform, float hpPercentage);
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
