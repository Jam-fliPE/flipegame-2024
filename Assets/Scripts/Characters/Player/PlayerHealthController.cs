using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected override void OnDie()
    {
        GetComponent<PlayerController>().ChangeState(EControllerState.NoInput);
        Invoke("LoadDefeat", 3.0f);
    }

    private void LoadDefeat()
    {
        ScreenManager.Instance.LoadDefeatScreen();
    }
}
