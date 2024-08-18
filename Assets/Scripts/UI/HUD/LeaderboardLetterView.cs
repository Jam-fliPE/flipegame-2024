using TMPro;
using UnityEngine;

public class LeaderboardLetterView : MonoBehaviour
{
    private TextMeshProUGUI _letter;
    
    public string Text {  get { return _letter.text; } }

    private void Awake()
    {
        _letter = GetComponent<TextMeshProUGUI>();
    }

    public void Increase()
    {
        char character = _letter.text[0];

        if (character < 'Z')
        {
            character++;
            _letter.text = character.ToString();
        }
    }

    public void Decrease()
    {
        char character = _letter.text[0];
        if (character > 'A')
        {
            character--;
            _letter.text = character.ToString();
        }
    }
}
