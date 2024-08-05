public delegate void OnScoreChange(int currentScore);

public class PlayerCombatController : CombatController
{
    public OnScoreChange _onScoreChange;

    public int Score { get; private set; }

    protected override void Start()
    {
        base.Start();
        Score = 0;
    }

    protected override void OnKilledEnemy()
    {
        Score++;
        _onScoreChange?.Invoke(Score);
    }
}
