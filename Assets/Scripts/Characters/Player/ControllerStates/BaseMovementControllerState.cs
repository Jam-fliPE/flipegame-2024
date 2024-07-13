using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseMovementState : AControllerState
{
    private Vector3 _inputDirection = Vector3.zero;
    private CharacterMovement _movement;
    private CombatController _combatController;

    public override void OnEnter(PlayerController controller)
    {
        _movement ??= controller.GetComponent<CharacterMovement>();
        _combatController ??= controller.GetComponent<CombatController>();

        SetupMovementDirection(controller);
    }

    public override void OnMove(PlayerController controller, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            SetupMovementDirection(controller);
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

    private void SetupMovementDirection(PlayerController controller)
    {
        _inputDirection.z = controller.InputVector.x;
        _inputDirection.x = -controller.InputVector.y;

        _inputDirection.Normalize();

        _movement.SetDirection(_inputDirection);
    }
}
