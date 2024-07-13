using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float _attackDistance = 2.0f;
    [SerializeField]
    private float _patrolDistance = 5.0f;

    public CharacterMovement MovementController { get; private set; }
    public Transform PlayerTransform { get; set; }
    public CombatController CombatController { get; private set; }
    public HealthController HealthController { get; private set; }
    public PlayerHealthController PlayerHealthController { get; private set; }
    public bool IsEngaged { get; set; }

    private Transform _transform;
    private BaseEnemyState _state;

    public float AttackDistance { get { return _attackDistance; } }
    public float PatrolDistance { get { return _patrolDistance; } }

    private void Start()
    {
        MovementController = GetComponent<CharacterMovement>();
        CombatController = GetComponent<CombatController>();
        HealthController = GetComponent<EnemyHealthController>();
        _transform = transform;
        GameObject player = GameplayManager.Instance.GetPlayer();
        PlayerTransform = player.transform;
        PlayerHealthController = player.GetComponent<PlayerHealthController>();

        _state = new IdleState();
    }

    private void Update()
    {
        if (HealthController.IsAlive())
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

    public bool AreCharactersAlive()
    {
        return (HealthController.IsAlive() && PlayerHealthController.IsAlive());
    }
}
