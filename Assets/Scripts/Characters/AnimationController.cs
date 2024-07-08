using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDirection(ref Vector3 inputDirection)
    {
        bool isMoving = (inputDirection != Vector3.zero);
        _animator.SetBool("move", isMoving);
    }
}
