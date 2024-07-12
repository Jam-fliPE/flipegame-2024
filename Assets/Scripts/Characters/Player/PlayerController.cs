using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private AControllerState _state;
    private ControllerStatesFactory _statesFactory;

    private void Start()
    {
        _statesFactory = new ControllerStatesFactory();
        ChangeState(EControllerState.BaseMovement);
    }

    private void Update()
    {
        _state.OnUpdate(this);
    }

    public void ChangeState(EControllerState stateType)
    {
        _state = _statesFactory.Resolve(stateType);
        _state.OnEnter(this);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _state.OnMove(this, context); ;
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        _state.OnLightAttack(this, context);
    }
}
