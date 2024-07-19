using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerinput;
    private PlayerController _playercontroller;

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
        int index = _playerinput.playerIndex;
        GameObject player = GameplayManager.Instance.InstantiatePlayer(index);
        _playercontroller = player.GetComponent<PlayerController>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _playercontroller.Move(context);
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        _playercontroller.LightAttack(context);
    }
}
