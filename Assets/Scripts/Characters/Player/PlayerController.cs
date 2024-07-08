using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement _movement;
    private Vector3 _inputDirection = Vector3.zero;

    private void Start()
    {
        _movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        _inputDirection.Set(0.0f, 0.0f, 0.0f);

        _inputDirection.z = Input.GetAxis("Horizontal");
        _inputDirection.x = -Input.GetAxis("Vertical");

        _inputDirection.Normalize();

        _movement.SetDirection(ref _inputDirection);
    }
}
