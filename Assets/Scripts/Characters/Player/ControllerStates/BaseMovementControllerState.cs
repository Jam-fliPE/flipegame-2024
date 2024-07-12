using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseMovementState : AControllerState
{
    private Vector3 _inputDirection = Vector3.zero;
    private CharacterMovement _movement;
    private AnimationController _animation;
    private CombatController _combatController;

    public override void OnEnter(PlayerController controller)
    {
        _movement ??= controller.GetComponent<CharacterMovement>();
        _animation ??= controller.GetComponent<AnimationController>();
        _combatController ??= controller.GetComponent<CombatController>();
    }

    public override void OnMove(PlayerController controller, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            _inputDirection.z = inputVector.x;
            _inputDirection.x = -inputVector.y;

            _inputDirection.Normalize();

            _movement.SetDirection(_inputDirection);
        }
        else if (context.canceled)
        {
            _movement.SetDirection(Vector3.zero);
        }
    }

    public override void OnLightAttack(PlayerController controller, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movement.SetDirection(Vector3.zero);

            Action callback = () => { controller.ChangeState(EControllerState.BaseMovement); };
            _combatController.LightAttack(callback);
            controller.ChangeState(EControllerState.NoInput);
        }
    }
}
