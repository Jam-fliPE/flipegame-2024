using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected override void OnDie()
    {
        Debug.Log("GAME OVER");
    }
}
