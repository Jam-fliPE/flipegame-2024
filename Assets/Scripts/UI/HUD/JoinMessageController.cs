using TMPro;
using UnityEngine;

public class JoinMessageController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerInfoViewsPrefab;
    [SerializeField]
    private Transform[] _infoViewTransforms;

    private TextMeshProUGUI _message;

    private void Start()
    {
        GameplayManager.Instance._onPlayerInstantiated += OnPlayerInstantiated;
        _message = GetComponent<TextMeshProUGUI>();
    }

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
