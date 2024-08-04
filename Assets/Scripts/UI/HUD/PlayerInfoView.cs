using TMPro;
using UnityEngine;

public class PlayerInfoView : MonoBehaviour
{
    [SerializeField]
    private Transform _hp;
    [SerializeField]
    private TextMeshProUGUI _score;

    public void Setup(Transform playerTransform)
    {
        _score.text = "0";
        PlayerHealthController healthController = playerTransform.GetComponent<PlayerHealthController>();
        healthController._onHPChange += OnHPChange;
        healthController._onDie += OnDie;
    }

    private void OnScoreChange(int newScore)
    {
        _score.text = newScore.ToString();
    }

    private void OnHPChange(float hpPercentage)
    {
        _hp.localScale = new Vector2(hpPercentage, 1.0f);
    }

    private void OnDie()
    {
        _hp.localScale = new Vector2(0.0f, 1.0f);
    }
}
