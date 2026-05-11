using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambidienceSlider;


    private void Start()
    {


    }

    public void SetSliderValues()
    {
        var data = AudioSaveManager.instance.Data;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20f);
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(sfxSlider.value) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20f);
        audioMixer.SetFloat("AmbVolume", Mathf.Log10(ambidienceSlider.value) * 20f);

    }
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        AudioSaveManager.instance.Data.masterVolume = level;
        SaveSystem.Save();
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(level) * 20f);
        AudioSaveManager.instance.Data.sfxVolume = level;
        SaveSystem.Save();
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        AudioSaveManager.instance.Data.musicVolume = level;
        SaveSystem.Save();
    }

    public void SetAmbidienceVolume(float level)
    {
        audioMixer.SetFloat("AmbVolume", Mathf.Log10(level) * 20f);
        AudioSaveManager.instance.Data.ambVolume = level;
        SaveSystem.Save();
    }
}