using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private string[] _lightAttacks = { "ze_light_attack_1", "ze_light_attack_2", "ze_light_attack_3" };
    private string[] _hitReactions = { "hit_reaction_1", "hit_reaction_2" };

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
        int index = UnityEngine.Random.Range(0, _lightAttacks.Length);
        _animator.Play(_lightAttacks[index]);
        yield return null;

        float duration = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        callback?.Invoke();
    }

    public void PlayHitReaction()
    {
        int index = UnityEngine.Random.Range(0, _hitReactions.Length);
        _animator.Play(_hitReactions[index]);
    }

    public void PlayDeath()
    {
        _animator.SetTrigger("death");
    }
}
