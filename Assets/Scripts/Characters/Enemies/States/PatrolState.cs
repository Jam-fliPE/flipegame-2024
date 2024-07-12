using System.Collections;
using UnityEngine;

public class PatrolState : BaseEnemyState
{
    public override void OnEnter(AIController controller)
    {
        Vector3 currentPosition = controller.transform.position;
        float x = Random.Range(-0.5f, 0.5f);
        float z = Random.Range(-1.0f, 1.0f);

        Vector3 direction = new Vector3(x, 0.0f, z);
        direction.Normalize();
        controller.MovementController.SetDirection(direction);

        controller.StartCoroutine(WaitAndChangeState(controller));
    }

    private IEnumerator WaitAndChangeState(AIController controller)
    {
        float time = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(time);

        controller.ChangeState(new IdleState());
    }
}
