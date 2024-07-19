using TMPro;
using UnityEngine;

public class JoinMessageController : MonoBehaviour
{
    private TextMeshProUGUI _message;
    private float _time;
    private bool _blinking;

    private void Start()
    {
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
        _time = 0.0f;
        _blinking = true;
        _message = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_blinking)
        {
            _time += Time.deltaTime;
            if (_time > 0.5f)
            {
                _time = 0.0f;
                _message.enabled = !_message.enabled;
            }
        }
    }

    private void OnPlayerInstantiated(Transform transform)
    {
        int playersCount = GameplayManager.Instance.GetPlayersCount();
        if (playersCount >= 2)
        {
            _blinking = false;
            _message.enabled = false;
        }
    }
}
