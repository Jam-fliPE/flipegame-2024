using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundData _audioClips;
    [SerializeField]
    private AudioSource[] _sfxSources;
    [SerializeField]
    private AudioSource[] _bgmSources;

    public static SoundManager Instance { get; private set; }

    private int _sfxIndex = 0;
    private int _bgmIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayGameplayBgm()
    {
        PlayBgm(_audioClips._gameplayBgm);
    }

    public void PlayMenuBgm()
    {
        PlayBgm(_audioClips._menuBgm);
    }

    public void PlayMenuNavigation()
    {
        PlaySfx(_audioClips._menuButtonsNavigtion);
    }

    public void PlayMenuSelection()
    {
        PlaySfx(_audioClips._menuButtonSelection);
    }

    public void PlayLightAttack()
    {
        PlaySfx(GetRandomClip(_audioClips._attacks));
        PlaySfx(GetRandomClip(_audioClips._clothMovements));
    }

    public void PlaySwordHit()
    {
        PlaySfx(GetRandomClip(_audioClips._swordHits));
    }

    public void PlayHardHit()
    {
        PlaySfx(GetRandomClip(_audioClips._hardHits));
    }

    public void PlayFootStep()
    {
        PlaySfx(GetRandomClip(_audioClips._footsteps));
    }

    private void PlaySfx(AudioClip clip)
    {
        AudioSource source = GetNextSource(_sfxSources, ref _sfxIndex);
        source.clip = clip;
        source.Play();
    }

    private void PlayBgm(AudioClip clip)
    {
        AudioSource source = GetNextSource(_bgmSources, ref _bgmIndex);
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int index = Random.Range(0, clips.Length);
        return clips[index];
    }

    private AudioSource GetNextSource(AudioSource[] sources, ref int index)
    {
        AudioSource result = sources[index];
        index++;
        if (index >= sources.Length)
        {
            index = 0;
        }

        return result;
    }
}
