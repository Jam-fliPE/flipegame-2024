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
    private int _letterIndex;

    private void Awake()
    {
        _time = 0.0f;
        _letterIndex = 0;
        _currentLetter = _letters[0];
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
    }

    public void SetScore(string newScore)
    {
        _score.text = newScore;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentLetter.gameObject.SetActive(true);
            if (_letterIndex == _letters.Length - 1)
            {
                _currentLetter = null;
                GetComponent<PlayerInput>().enabled = false;
            }
            else
            {
                _letterIndex++;
                _currentLetter = _letters[_letterIndex];
            }
        }
    }
}
