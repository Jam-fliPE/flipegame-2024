using TMPro;
using UnityEngine;

public class LeaderboardEntryView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _position;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _score;

    public void Setup(string position, string name, string score)
    {
        _position.text = position;
        _name.text = name;
        _score.text = score;
    }
}
