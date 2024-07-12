using UnityEngine;

public class MoveToPlayerState : BaseEnemyState
{
    public override void OnUpdate(AIController controller)
    {
        Vector3 targetPosition = controller.PlayerTransform.position;
        if (Vector3.Distance(targetPosition, controller.transform.position) < controller.AttackDistance)
        {
            controller.ChangeState(new AttackState());
        }
        else if (controller.IsEngaged)
        {
            Vector3 direction = targetPosition - controller.transform.position;
            direction.Normalize();
            controller.MovementController.SetDirection(direction);
        }
    }
}
