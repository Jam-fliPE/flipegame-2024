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

    private PlayerHealthController _healthController;
    private PlayerCombatController _combatController;

    public LeaderboardInputView InputView { get { return _leaderboardControllerView.GetComponent<LeaderboardInputView>(); } }

    public void Setup(Transform playerTransform)
    {
        _score.text = "0";
        _healthController = playerTransform.GetComponent<PlayerHealthController>();
        _healthController._onHPChange += OnHPChange;
        _healthController._onDie += OnDie;

        _combatController = playerTransform.GetComponent<PlayerCombatController>();
        _combatController._onScoreChange += OnScoreChange;
        playerTransform.GetComponent<PlayerController>().PlayerInfoView = this;
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
        _healthController._onHPChange -= OnHPChange;
        _healthController._onDie -= OnDie;
        _combatController._onScoreChange -= OnScoreChange;

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
