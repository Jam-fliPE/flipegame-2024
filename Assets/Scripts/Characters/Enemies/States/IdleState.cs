using UnityEngine;

public class IdleState : BaseEnemyState
{
    private float MAX_WAIT_TIME = 2.0f;
    private float _waitTime = 0.0f;

    public override void OnEnter(AIController controller)
    {
        controller.MovementController.SetDirection(Vector3.zero);

        _waitTime = 0.0f;
    }

    public override void OnUpdate(AIController controller)
    {
        _waitTime += Time.deltaTime;

        if (controller.AreCharactersAlive())
        {
            if (Vector3.Distance(controller.PlayerTransform.position, controller.transform.position) < controller.AttackDistance)
            {
                controller.ChangeState(new AttackState());
            }
            else if (controller.IsEngaged)
            {
                controller.ChangeState(new MoveToPlayerState());
            }
            else if (_waitTime > MAX_WAIT_TIME)
            {
                _waitTime = 0.0f;
                controller.ChangeState(new PatrolState());
            }
        }
    }
}
