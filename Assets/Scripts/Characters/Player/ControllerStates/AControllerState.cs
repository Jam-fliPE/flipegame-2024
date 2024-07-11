using UnityEngine;

public enum EControllerState
{
    None,
    BaseMovement,
    NoInput
}

public abstract class AControllerState
{
    public virtual void OnEnter(PlayerController controller) { }
    
    public virtual void OnUpdate(PlayerController controller)
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ScreenManager.Instance.LoadMainMenu();
        }
    }
}
