using TMPro;
using UnityEngine;

public class LeaderboardLetterView : MonoBehaviour
{
    private TextMeshProUGUI _letter;

    private void Awake()
    {
        _letter = GetComponent<TextMeshProUGUI>();
    }

    public void Increase()
    {
        char character = _letter.text[0];
        character++;
        _letter.text = character.ToString();
    }

    public void Decrease()
    {
        char character = _letter.text[0];
        character--;
        _letter.text = character.ToString();
    }
}
