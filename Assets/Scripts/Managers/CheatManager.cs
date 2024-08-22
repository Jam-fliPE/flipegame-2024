using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            EnemyHealthController[] enemies = FindObjectsOfType<EnemyHealthController>();
            foreach (EnemyHealthController item in enemies)
            {
                bool killed;
                item.TakeDamage(transform, 99999, out killed);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            bool killed;
            HealthController healthController = GameplayManager.Instance.GetPlayer().GetComponent<HealthController>();
            healthController.TakeDamage(transform, 9999, out killed);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            DatabaseManager.Instance.DEBUG_ClearDatabase();
            Application.Quit();
        }
    }
}
