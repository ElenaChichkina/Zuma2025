using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioSource effectsSource;

    public AudioClip[] musicTracks;
    public AudioClip[] soundClips;
    public AudioClip[] effectClips;

    private int currentMusicTrack = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolumeSettings();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicTracks.Length > 0)
        {
            musicSource.clip = musicTracks[currentMusicTrack];
            musicSource.Play();
        }
    }

    public void NextTrack()
    {
        currentMusicTrack = (currentMusicTrack + 1) % musicTracks.Length;
        PlayMusic();
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < soundClips.Length)
        {
            soundSource.PlayOneShot(soundClips[index]);
        }
    }

    public void PlayEffect(int index)
    {
        if (index >= 0 && index < effectClips.Length)
        {
            effectsSource.PlayOneShot(effectClips[index]);
        }
    }

    private void LoadVolumeSettings()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        effectsSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 1f);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        soundSource.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }
}
