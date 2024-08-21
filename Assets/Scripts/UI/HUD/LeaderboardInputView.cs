using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeaderboardInputView : MonoBehaviour
{
    [SerializeField]
    private LeaderboardLetterView[] _letters;
    [SerializeField]
    private TextMeshProUGUI _score;

    private LeaderboardLetterView _currentLetter;
    private float _time;
    private float _inputTime;
    private int _letterIndex;
    private bool _inputEnabled;

    private void Awake()
    {
        _time = 0.0f;
        _letterIndex = 0;
        _inputTime = 0.0f;
        _currentLetter = _letters[0];
    }

    private void Start()
    {
        GameplayManager.Instance.OnPlayerScoreInputBegin();
        _inputEnabled = true;
    }

    private void Update()
    {
        if (_currentLetter != null)
        {
            _time += Time.deltaTime;
            if (_time > 0.5f)
            {
                _time = 0.0f;
                _currentLetter.gameObject.SetActive(!_currentLetter.gameObject.activeSelf);
            }

        }
        _inputTime += Time.deltaTime;
    }

    public void SetScore(string newScore)
    {
        _score.text = newScore;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_inputEnabled && context.performed && (_inputTime > 0.1f))
        {
            _inputTime = 0.0f;
            Vector2 direction = context.ReadValue<Vector2>();
            if (direction.x > 0.5f)
            {
                _letterIndex++;
            }
            else if (direction.x < -0.5f)
            {
                _letterIndex--;
            }
            else if (direction.y > 0.5f)
            {
                _currentLetter.Increase();
            }
            else if (direction.y < -0.5f)
            {
                _currentLetter.Decrease();
            }

            _letterIndex = Mathf.Clamp(_letterIndex, 0, _letters.Length - 1);

            _currentLetter.gameObject.SetActive(true);
            _currentLetter = _letters[_letterIndex];

            SoundManager.Instance.PlayMenuNavigation();
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (_inputEnabled && context.performed)
        {
            _currentLetter.gameObject.SetActive(true);
            if (_letterIndex == _letters.Length - 1)
            {
                _inputEnabled = false;
                SoundManager.Instance.PlayMenuSelection();
                _currentLetter = null;
                SaveData();
                GameplayManager.Instance.OnPlayerScoreInputEnd();
            }
            else
            {
                SoundManager.Instance.PlayMenuNavigation();
                _letterIndex++;
                _currentLetter = _letters[_letterIndex];
            }
        }
    }

    private void SaveData()
    {
        string name = string.Format("{0}{1}{2}", _letters[0].Text, _letters[1].Text, _letters[2].Text);
        string score = _score.text;

        DatabaseManager.Instance.SaveLeaderboardEntry(name, int.Parse(score));
    }
}
