using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDirection(ref Vector3 inputDirection)
    {
        bool isMoving = (inputDirection != Vector3.zero);
        _animator.SetBool("move", isMoving);
    }

    public IEnumerator PlayLightAttack(Action callback)
    {
        _animator.SetTrigger("lightAttack");
        yield return null;

        float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        callback();
    }
}
