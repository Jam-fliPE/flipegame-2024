using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerinput;
    private PlayerController _playercontroller;

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
        PlayerController[] controllers = FindObjectsOfType<PlayerController>();
        int index = _playerinput.playerIndex;
        _playercontroller = controllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
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
