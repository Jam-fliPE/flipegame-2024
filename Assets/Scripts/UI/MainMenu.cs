using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _controlsPanel;

    [SerializeField]
    private GameObject _creditsPanel;

    [SerializeField]
    private Image _startButton;
    [SerializeField]
    private TextMeshProUGUI _startText;

    [SerializeField]
    private Image _controlsButton;
    [SerializeField]
    private TextMeshProUGUI _controlsText;

    [SerializeField]
    private Image _creditsButton;
    [SerializeField]
    private TextMeshProUGUI _creditsText;

    [SerializeField]
    private Image _quitButton;
    [SerializeField]
    private TextMeshProUGUI _quitText;

    private Image[] _buttons;
    private TextMeshProUGUI[] _buttonsText;

    private Image _currentButton;
    private TextMeshProUGUI _currentText;
    private Image _previousButton;
    private TextMeshProUGUI _previousText;

    private bool _navigationEnabled;

    private void Start()
    {
        _buttons = new Image[4] { _startButton, _controlsButton, _creditsButton, _quitButton };
        _buttonsText = new TextMeshProUGUI[4] { _startText, _controlsText, _creditsText, _quitText };

        _currentButton = _controlsButton;
        _currentText = _controlsText;
        _previousButton = null;
        _previousText = null;
        SetSelection(_startButton, _startText, false);
        SoundManager.Instance.PlayMenuBgm();
        _navigationEnabled = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_navigationEnabled && context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            if (direction.y > 0.5f)
            {
                SetSelection(_startButton, _startText);
            }
            else if (direction.y < -0.5f)
            {
                if (_currentButton == _startButton)
                {
                    SetSelection(_previousButton, _previousText);
                }
            }
            else if (direction.x > 0.5f)
            {
                if (_currentButton == _controlsButton)
                {
                    SetSelection(_creditsButton, _creditsText);
                }
                else if (_currentButton == _creditsButton)
                {
                    SetSelection(_quitButton, _quitText);
                }
            }
            else if (direction.x < -0.5f)
            {
                if (_currentButton == _quitButton)
                {
                    SetSelection(_creditsButton, _creditsText);
                }
                else if (_currentButton == _creditsButton)
                {
                    SetSelection(_controlsButton, _controlsText);
                }
            }
        }
    }

    public void CommandStart(InputAction.CallbackContext context)
    {
        Select(context);
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (_navigationEnabled)
        {
            SoundManager.Instance.PlayMenuSelection();

            ScreenManager screenManager = ScreenManager.Instance;
            if (_currentButton == _startButton)
            {
                StartCoroutine(WaitAndCallAction(() => screenManager.LoadGame()));
            }
            else if (_currentButton == _controlsButton)
            {
                StartCoroutine(WaitAndCallAction(() => _controlsPanel.SetActive(true)));
            }
            else if (_currentButton == _creditsButton)
            {
                StartCoroutine(WaitAndCallAction(() => _creditsPanel.SetActive(true)));
            }
            else if (_currentButton == _quitButton)
            {
                StartCoroutine(WaitAndCallAction(() => screenManager.MainMenuQuit()));
            }
        }
    }

    private IEnumerator WaitAndCallAction(Action action)
    {
        _navigationEnabled = false;
        _currentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        _currentButton.transform.localScale = Vector3.one;
        _navigationEnabled = true;
        action();
    }

    private void ResetSelection()
    {
        foreach (Image item in _buttons)
        {
            item.color = Color.white;
        }

        foreach (TextMeshProUGUI item in _buttonsText)
        {
            item.color = Color.white;
        }
    }

    private void SetSelection(Image button, TextMeshProUGUI text, bool playSound = true)
    {
        if (_currentButton != button)
        {
            ResetSelection();
            _previousButton = _currentButton;
            _previousText = _currentText;

            _currentButton = button;
            _currentText = text;

            _currentButton.color = Color.green;
            _currentText.color = Color.green;

            if (playSound)
            {
                SoundManager.Instance.PlayMenuNavigation();
            }
        }
    }
}
