using System.Collections.Generic;
using UnityEngine;


public class AudioSaveManager : MonoBehaviour
{

    public static AudioSaveManager instance;

    public SoundSaveData Data { get; private set; } = new SoundSaveData();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 120;
    }



    private void Start()
    {


        SaveSystem.Load();
    }

    public void LoadData(SoundSaveData data)
    {
        Data = data;
    }

    public void ResetData()
    {
        Data = new SoundSaveData
        {
            masterVolume = 0.5f,
            musicVolume = 0.5f,
            sfxVolume = 0.5f,
            ambVolume = 0.5f,

        };

    }

}
