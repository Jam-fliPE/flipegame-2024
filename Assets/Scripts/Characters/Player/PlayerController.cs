using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _playerIndex = 0;

    public Vector2 InputVector { get; private set; }
    public Transform PartnerTransform { get; set; }
    public CharacterController PartnerController { get; set; }
    public PlayerInfoView PlayerInfoView { get; set; }

    private AControllerState _state;
    private ControllerStatesFactory _statesFactory;
    private Transform _transform;
    private CharacterController _characterController;

    private void Start()
    {
        _transform = transform;
        _statesFactory = new ControllerStatesFactory();
        _characterController = GetComponent<CharacterController>();
        ChangeState(EControllerState.BaseMovement);

        BordersNavigationManager.Instance.AddPlayer(transform);
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
    }

    private void Update()
    {
        _state.OnUpdate(this);

        if (PartnerTransform != null)
        {
            CheckDistanceToPartner();
        }
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    public void ChangeState(EControllerState stateType)
    {
        _state = _statesFactory.Resolve(stateType);
        _state.OnEnter(this);
    }

    public void Move(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
        _state.OnMove(this, context);
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        _state.OnLightAttack(this, context);
    }

    private void OnPlayerInstantiated(Transform playerTransform)
    {
        PartnerTransform = playerTransform;
        PartnerController = PartnerTransform.GetComponent<CharacterController>();

        PlayerController partnerPlayerController = PartnerTransform.GetComponent<PlayerController>();
        partnerPlayerController.PartnerTransform = _transform;
        partnerPlayerController.PartnerController = _characterController;
    }

    private void CheckDistanceToPartner()
    {
        if (Mathf.Abs(_transform.position.z - PartnerTransform.position.z) > 5.0f)
        {
            if (_characterController.velocity.z != 0.0f)
            {
                Vector3 newPosition = _transform.position;
                newPosition.z = PartnerTransform.position.z + Mathf.Sign(_transform.position.z - PartnerTransform.position.z) * 5.0f;
                _transform.position = newPosition;
            }
        }
    }
}
