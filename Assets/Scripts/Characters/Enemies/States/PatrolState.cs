using UnityEngine;

public class PatrolState : BaseEnemyState
{
    private Vector3 _targetPosition;

    public override void OnEnter(AIController controller)
    {

    }

    public override void OnUpdate(AIController controller)
    {
    }

    private void MoveToNextPosition(AIController controller)
    {
        Vector3 currentPosition = controller.transform.position;
        float newXOffset = Random.Range(-controller.PatrolDistance, controller.PatrolDistance);
        float newZOffset = Random.Range(-controller.PatrolDistance, controller.PatrolDistance);

        _targetPosition = currentPosition + new Vector3(newXOffset, 0.0f, newZOffset);
    }
}
