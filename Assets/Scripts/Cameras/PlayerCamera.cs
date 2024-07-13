using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform1;
    [SerializeField]
    private Transform _playerTransform2;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    void LateUpdate()
    {
        Vector3 position = _transform.position;
        position.z = (_playerTransform1.position.z + _playerTransform2.position.z) * 0.5f;

        _transform.position = position;
    }
}
