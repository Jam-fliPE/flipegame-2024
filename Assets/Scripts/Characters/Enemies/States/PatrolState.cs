using UnityEngine;

public class PatrolState : BaseEnemyState
{
    private Vector3 _targetPosition;

    public override void OnEnter(AIController controller)
    {
        Vector3 currentPosition = controller.transform.position;
        float newXOffset = Random.Range(-controller.PatrolDistance * 0.5f, controller.PatrolDistance * 0.5f);
        float newZOffset = Random.Range(-controller.PatrolDistance, controller.PatrolDistance);

        _targetPosition = currentPosition + new Vector3(newXOffset, 0.0f, newZOffset);
        Vector3 direction = _targetPosition - controller.transform.position;
        direction.Normalize();
        controller.MovementController.SetDirection(direction);
    }

    public override void OnUpdate(AIController controller)
    {
        if (Vector3.Distance(_targetPosition, controller.transform.position) < 0.5f)
        {
            controller.ChangeState(new IdleState());
        }
    }
}
