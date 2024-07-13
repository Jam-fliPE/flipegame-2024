public class NoInputState : AControllerState
{
    private CharacterMovement _movement;

    public override void OnEnter(PlayerController controller)
    {
        _movement ??= controller.GetComponent<CharacterMovement>();
        _movement.SetDirection(UnityEngine.Vector3.zero);
    }
}
