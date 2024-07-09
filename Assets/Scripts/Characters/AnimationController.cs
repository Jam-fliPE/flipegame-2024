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

    public void PlayIdle()
    {
        _animator.Play("BasicMotions@Idle01");
    }

    public IEnumerator PlayLightAttack(Action callback)
    {
        _animator.Play("Take 001");
        yield return null;

        float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        callback();
    }
}
