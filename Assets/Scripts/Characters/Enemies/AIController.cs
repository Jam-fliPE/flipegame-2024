using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private float _attackDistance = 1.5f;
    [SerializeField]
    private float _patrolDistance = 5.0f;

    public CharacterMovement MovementController { get; private set; }
    public CombatController CombatController { get; private set; }
    public HealthController HealthController { get; private set; }
    public Transform PlayerTransform { get; private set; }
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

        GameplayManager.Instance._onPlayerDeath += OnPlayerDeath;
        SetPlayerTarget(GameplayManager.Instance.GetPlayer());

        _state = new IdleState();
    }

    private void Update()
    {
        if (HealthController.IsAlive())
        {
            if ((PlayerHealthController != null) && PlayerHealthController.IsAlive())
            {
                _transform.LookAt(PlayerTransform.position, Vector3.up);
                _state.OnUpdate(this);
            }
        }
    }

    public void SetPlayerTarget(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
        PlayerHealthController = (playerTransform != null) ? PlayerTransform.GetComponent<PlayerHealthController>() : null;
    }

    public void ChangeState(BaseEnemyState newState)
    {
        _state = newState;
        _state.OnEnter(this);
    }

    public bool AreCharactersAlive()
    {
        return (HealthController.IsAlive() && (PlayerHealthController != null) && PlayerHealthController.IsAlive());
    }

    private void OnPlayerDeath(Transform deadPlayer)
    {
        if (deadPlayer == PlayerTransform)
        {
            Transform nextPlayer = GameplayManager.Instance.GetPlayer();
            SetPlayerTarget(nextPlayer);

            if (nextPlayer == null)
            {
                ChangeState(new IdleState());
            }
        }
    }
}
