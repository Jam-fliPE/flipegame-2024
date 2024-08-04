using UnityEngine;

public delegate void OnDie();
public delegate void OnHPChange(float hpPercentage);

public class PlayerHealthController : HealthController
{
    public OnDie _onDie;
    public OnHPChange _onHPChange;

    protected override void OnTakeDamage(Transform opponentTransform, float hpPercentage)
    {
        _onHPChange?.Invoke(hpPercentage);
    }

    protected override void OnDie()
    {
        GetComponent<PlayerController>().ChangeState(EControllerState.NoInput);

        _onDie?.Invoke();
        BordersNavigationManager.Instance.RemovePlayer(transform);
        GameplayManager.Instance.OnPlayerDeath(transform);
    }
}
