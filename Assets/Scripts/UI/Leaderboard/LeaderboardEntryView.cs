using TMPro;
using UnityEngine;

public class LeaderboardEntryView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _score;

    public void Setup(string name, string score)
    {
        _name.text = name;
        _score.text = score;
    }
}
