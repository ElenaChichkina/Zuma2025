using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider effectsSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        musicSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetMusicVolume(value));
        soundSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetSoundVolume(value));
        effectsSlider.onValueChanged.AddListener(value => AudioManager.Instance.SetEffectsVolume(value));
    }
}