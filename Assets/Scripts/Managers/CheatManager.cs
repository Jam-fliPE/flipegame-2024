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
    }
}
