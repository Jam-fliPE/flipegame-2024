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

    public void PlayBackground()
    {
        PlayBgm(_audioClips._background);
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
        // PlaySfx(_audioClips._lightAttack);
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
        source.Play();
    }

    private AudioClip GetRandomSound(AudioClip[] clips)
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
