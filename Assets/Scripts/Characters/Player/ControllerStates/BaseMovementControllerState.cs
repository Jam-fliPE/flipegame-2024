using System;
using UnityEngine;

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

    public override void OnUpdate(PlayerController controller)
    {
        base.OnUpdate(controller);

        _inputDirection.Set(0.0f, 0.0f, 0.0f);

        _inputDirection.z = Input.GetAxis("Horizontal");
        _inputDirection.x = -Input.GetAxis("Vertical");

        _inputDirection.Normalize();


        if (Input.GetButtonDown("Jump"))
        {
            _inputDirection.Set(0.0f, 0.0f, 0.0f);
            Action callback = () => { controller.ChangeState(EControllerState.BaseMovement); };
            _combatController.LightAttack(callback);
            controller.ChangeState(EControllerState.NoInput);
        }

        _movement.SetDirection(ref _inputDirection);
    }
}
