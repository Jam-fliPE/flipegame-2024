using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float _attackDistance = 2.0f;
    [SerializeField]
    private float _patrolDistance = 5.0f;

    public CharacterMovement MovementController { get; private set; }
    public Transform PlayerTransform { get; private set; }
    public CombatController CombatController { get; private set; }

    private EnemyHealthController _healthController;
    private Vector3 _inputDirection = Vector3.zero;
    private Transform _transform;
    private BaseEnemyState _state;

    public float AttackDistance { get { return _attackDistance; } }
    public float PatrolDistance { get { return _patrolDistance; } }

    private void Start()
    {
        MovementController = GetComponent<CharacterMovement>();
        CombatController = GetComponent<CombatController>();
        _healthController = GetComponent<EnemyHealthController>();
        _transform = transform;
        GameObject player = GameplayManager.Instance.GetPlayer();
        PlayerTransform = player.transform;

        _state = new IdleState();
    }

    private void Update()
    {
        if (_healthController.IsAlive())
        {
            _transform.LookAt(PlayerTransform.position, Vector3.up);
            _state.OnUpdate(this);
        }
    }

    public void ChangeState(BaseEnemyState newState)
    {
        _state = newState;
        _state.OnEnter(this);
    }
}
