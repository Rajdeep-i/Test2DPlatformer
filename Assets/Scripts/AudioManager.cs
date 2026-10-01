using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private bool soundOn = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // MUSIC
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // SOUND ON/OFF
    public void ToggleSound()
    {
        soundOn = !soundOn;
        AudioListener.volume = soundOn ? 1f : 0f;
    }

    // PLAY SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && soundOn)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public bool IsSoundOn()
    {
        return soundOn;
    }
}