using UnityEngine;

public class AttackState : BaseEnemyState
{
    private CombatController _combatController;

    public override void OnEnter(AIController controller)
    {
        _combatController ??= controller.GetComponent<CombatController>();
    }

    private void Attack(AIController controller)
    {
        GameObject player = GameplayManager.Instance.GetPlayer();
        if (Vector3.Distance(player.transform.position, controller.transform.position) < controller.AttackDistance)
        {
            _combatController.LightAttack(() => { Attack(controller); });
        }
        else
        {

        }

    }
}
