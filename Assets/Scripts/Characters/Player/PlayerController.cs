using UnityEngine;

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
}
