using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _rotationSpeed = 10.0f;
    [SerializeField]
    private float _inputDeadZone = 0.5f;

    private CharacterController _characterController;
    private Transform _transform;
    private AnimationController _animationController;
    private Vector3 _direction = Vector3.forward;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animationController= GetComponent<AnimationController>();
        _transform = transform;
    }

    private void Update()
    {
        _direction.Set(0.0f, 0.0f, 0.0f);

        _direction.z = Input.GetAxis("Horizontal");
        _direction.x = -Input.GetAxis("Vertical");

        _direction.Normalize();

        if (_direction != Vector3.zero)
        {
            _transform.forward = Vector3.Slerp(_transform.forward, _direction, Time.deltaTime * _rotationSpeed);
        }

        Vector3 velocity = _direction * _speed * Time.deltaTime;
        _characterController.Move(velocity);

        _animationController.UpdateAnimation(ref _direction);
    }

    private bool IsInDeadZone(float value)
    {
        return (Mathf.Abs(value) > _inputDeadZone);
    }
}
