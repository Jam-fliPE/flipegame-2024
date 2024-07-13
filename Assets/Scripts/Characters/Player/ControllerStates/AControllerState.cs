using UnityEngine;
using UnityEngine.InputSystem;

public enum EControllerState
{
    None,
    BaseMovement,
    NoInput
}

public abstract class AControllerState
{
    public virtual void OnEnter(PlayerController controller) { }
    
    public virtual void OnUpdate(PlayerController controller) { }

    public virtual void OnMove(PlayerController controller, InputAction.CallbackContext context) { }
    public virtual void OnLightAttack(PlayerController controller, InputAction.CallbackContext context) { }
}
