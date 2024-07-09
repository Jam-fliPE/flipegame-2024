using UnityEngine;

public class AIController : MonoBehaviour
{
    private CharacterMovement _movement;
    private Vector3 _inputDirection = Vector3.zero;
    private Transform _transform;
    private Transform _playerTransform;

    private void Start()
    {
        _movement = GetComponent<CharacterMovement>();
        _transform = transform;
        GameObject player = GameplayManager.Instance.GetPlayer();
        _playerTransform = player.transform;
    }

    private void Update()
    {
        _transform.LookAt(_playerTransform.position, Vector3.up);
    }
}
