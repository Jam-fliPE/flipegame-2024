using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerinput;
    private PlayerController _playercontroller;
    private bool _alive;
    private LeaderboardInputView _inputview;
    private PlayerHealthController _healthController;

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
        int index = _playerinput.playerIndex;
        Transform playerTransform = GameplayManager.Instance.InstantiatePlayer(index);
        _playercontroller = playerTransform.GetComponent<PlayerController>();

        _healthController = playerTransform.GetComponent<PlayerHealthController>();
        _healthController._onDie += OnDie;
        _alive = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_alive)
        {
            _playercontroller.Move(context);
        }
        else
        {
            _inputview.Move(context);
        }
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        if (_alive)
        {
            _playercontroller.LightAttack(context);
        }
        else
        {
            _inputview.Select(context);
        }
    }

    private void OnDie()
    {
        _healthController._onDie -= OnDie;
        _inputview = _playercontroller.PlayerInfoView.InputView;
        _alive = false;
    }
}
