using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected override void OnDie()
    {
        Invoke("LoadDefeat", 3.0f);
    }

    private void LoadDefeat()
    {
        ScreenManager.Instance.LoadDefeatScreen();
    }
}
