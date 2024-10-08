using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _rotationSpeed = 10.0f;

    private CharacterController _characterController;
    private HealthController _healthController;
    private Transform _transform;
    private AnimationController _animationController;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animationController = GetComponent<AnimationController>();
        _healthController = GetComponent<HealthController>();
        _transform = transform;
    }

    private void Update()
    {
        if (_healthController.IsAlive() && !_healthController.OnHit)
        {
            if (_direction != Vector3.zero)
            {
                _transform.forward = Vector3.Slerp(_transform.forward, _direction, Time.deltaTime * _rotationSpeed);
            }

            Vector3 velocity = _direction * _speed * Time.deltaTime;
            if (!_characterController.isGrounded)
            {
                velocity.y = -1.0f;
            }
            _characterController.Move(velocity);

            _animationController.SetDirection(ref _direction);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection;
    }
}
