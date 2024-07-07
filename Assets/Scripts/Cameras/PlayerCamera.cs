using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    void LateUpdate()
    {
        Vector3 position = _transform.position;
        position.z = _playerTransform.position.z;
        _transform.position = position;
    }
}
