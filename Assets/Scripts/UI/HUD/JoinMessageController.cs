using TMPro;
using UnityEngine;

public class JoinMessageController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerInfoViewsPrefab;
    [SerializeField]
    private Transform[] _infoViewTransforms;

    private TextMeshProUGUI _message;
    /*
    private float _time;
    private bool _blinking;
    */
    private void Start()
    {
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
        _message = GetComponent<TextMeshProUGUI>();
        /*
        _time = 0.0f;
        _blinking = true;
        */
    }

    /*
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
    */

    private void OnPlayerInstantiated(Transform playerTransform)
    {
        GameObject playerInfoPrefab = _playerInfoViewsPrefab[0];
        Transform infoViewTransform = _infoViewTransforms[0];
        int playersCount = GameplayManager.Instance.GetPlayersCount();
        if (playersCount >= 2)
        {
            // _blinking = false;
            _message.enabled = false;

            playerInfoPrefab = _playerInfoViewsPrefab[1];
            infoViewTransform = _infoViewTransforms[1];
        }

        GameObject infoViewObject = Instantiate(playerInfoPrefab, infoViewTransform);
        PlayerInfoView infoView = infoViewObject.GetComponent<PlayerInfoView>();
        infoView.Setup(playerTransform);
    }
}
