using UnityEngine;

public static class UIInputMoveController
{
    private static bool _wasInDeadZone = true;
    private static float MAX_INPUT_DEADZONE = 0.2f;

    public static void Update(bool canceled)
    {
        _wasInDeadZone |= canceled;
    }

    public static bool CanMove(ref Vector2 direction)
    {
        bool result = false;
        bool isInDeadZone = IsInDeadZone(ref direction);

        if (_wasInDeadZone && !isInDeadZone)
        {
            result = true;
            _wasInDeadZone = false;

            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                direction.x = 0.0f;
            }
            else
            {
                direction.y = 0.0f;
            }
        }
        else if (!_wasInDeadZone && isInDeadZone)
        {
            _wasInDeadZone = true;
        }

        return result;
    }

    private static bool IsInDeadZone(ref Vector2 direction)
    {
        return
        (
            (Mathf.Abs(direction.x) < MAX_INPUT_DEADZONE) && (Mathf.Abs(direction.y) < MAX_INPUT_DEADZONE)
        );
    }
}
