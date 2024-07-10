using System.Collections.Generic;

public class ControllerStatesFactory
{
    private Dictionary<EControllerState, AControllerState> _states;

    public ControllerStatesFactory()
    {
        _states = new Dictionary<EControllerState, AControllerState>()
        {
            { EControllerState.BaseMovement, new BaseMovementState()},
            { EControllerState.NoInput, new NoInputState() }
        };
    }

    public AControllerState Resolve(EControllerState stateType)
    {
        return _states[stateType];
    }
}
