using TMPro;
using UnityEngine;

public class PlayerInfoView : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameplayView;
    [SerializeField]
    private GameObject _leaderboardControllerView;

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

        PlayerCombatController combatController = playerTransform.GetComponent<PlayerCombatController>();
        combatController._onScoreChange += OnScoreChange;
    }

    private void OnScoreChange(int currentScore)
    {
        _score.text = currentScore.ToString();
    }

    private void OnHPChange(float hpPercentage)
    {
        _hp.localScale = new Vector2(hpPercentage, 1.0f);
    }

    private void OnDie()
    {
        _hp.localScale = new Vector2(0.0f, 1.0f);
        CheckScore();
    }

    private void CheckScore()
    {
        if (DatabaseManager.Instance.IsScoreSuitable(int.Parse(_score.text)))
        {
            _gameplayView.SetActive(false);
            _leaderboardControllerView.SetActive(true);
            _leaderboardControllerView.GetComponent<LeaderboardInputView>().SetScore(_score.text);
        }
    }
}
