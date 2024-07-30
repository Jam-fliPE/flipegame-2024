using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected override void OnDie()
    {
        GetComponent<PlayerController>().ChangeState(EControllerState.NoInput);

        BordersNavigationManager.Instance.RemovePlayer(transform);
        GameplayManager.Instance.OnPlayerDeath(transform);
    }
}
