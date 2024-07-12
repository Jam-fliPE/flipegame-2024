using System;
using System.Collections;
using UnityEngine;

public class AttackState : BaseEnemyState
{
    private WaitForSeconds _attackDelay = new WaitForSeconds(1.0f);

    public override void OnEnter(AIController controller)
    {
        controller.StartCoroutine(WaitAndAttack(controller));
    }

    private IEnumerator WaitAndAttack(AIController controller)
    {
        yield return _attackDelay;
        Action callback = () => { controller.StartCoroutine(WaitForNextState(controller)); };
        controller.CombatController.LightAttack(callback);
    }

    private IEnumerator WaitForNextState(AIController controller)
    {
        yield return _attackDelay;
        controller.ChangeState(new IdleState());
    }
}
