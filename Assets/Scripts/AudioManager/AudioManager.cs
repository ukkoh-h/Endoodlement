using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds, ambSounds;
    public AudioSource musicSource, sfxSource, ambSource;
    [SerializeField] float pitchVariance = 0.5f;
    private AudioClip activeSound;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        var data = AudioSaveManager.instance.Data;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(data.masterVolume) * 20f);
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(data.sfxVolume) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(data.musicVolume) * 20f);
        audioMixer.SetFloat("AmbianceVolume", Mathf.Log10(data.ambVolume) * 20f);


        musicSource.volume = data.musicVolume;
        sfxSource.volume = data.sfxVolume;
        ambSource.volume = data.ambVolume;
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");
            return;

        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();

        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");

            return;
        }

        else
        {
            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
            sfxSource.PlayOneShot(s.clip);
            sfxSource.pitch = randomPitch;
            sfxSource.Play();


        }
    }

    public void PlayAmb(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");
            return;

        }
        if (ambSource.clip == s.clip) return; // prevent restart

        ambSource.clip = s.clip;
        ambSource.loop = true;
        ambSource.Play();

    }
    // Update is called once per frame
    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
