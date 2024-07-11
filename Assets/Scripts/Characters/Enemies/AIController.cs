using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float _attackDistance = 2.0f;
    [SerializeField]
    private float _patrolDistance = 5.0f;

    private CharacterMovement _movement;
    private EnemyHealthController _healthController;
    private Vector3 _inputDirection = Vector3.zero;
    private Transform _transform;
    private Transform _playerTransform;
    private BaseEnemyState _state;

    public float AttackDistance { get { return _attackDistance; } }
    public float PatrolDistance { get { return _patrolDistance; } }

    private void Start()
    {
        _movement = GetComponent<CharacterMovement>();
        _healthController = GetComponent<EnemyHealthController>();
        _transform = transform;
        GameObject player = GameplayManager.Instance.GetPlayer();
        _playerTransform = player.transform;

        // _state = 
    }

    private void Update()
    {
        if (_healthController.IsAlive())
        {
            _transform.LookAt(_playerTransform.position, Vector3.up);
            _state.OnUpdate(this);
        }
    }

    public void ChangeState(BaseEnemyState newState)
    {
        _state = newState;
        _state.OnEnter(this);
    }
}
