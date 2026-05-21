using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameUIManager : MonoBehaviour
{

    public static GameUIManager instance;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambientSlider;


    [SerializeField] private AudioMixer audioMixer;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject pausePanel;

    public bool cursorLocked = true;
    public bool cursorOpen = false;

    [HideInInspector] public bool GamePaused;

    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

   /* private void Start()
    {

        var data = AudioSaveManager.instance.Data;

        masterSlider.value = data.masterVolume;
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
        ambientSlider.value = data.ambVolume;

    } */
    private void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SetPauseGame(!GamePaused);
            SetCursorState(cursorOpen);
        }
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
        AudioManager.Instance.sfxSource.volume = level;
        AudioSaveManager.instance.Data.sfxVolume = level;
        SaveSystem.Save();
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        AudioManager.Instance.musicSource.volume = level;
        AudioSaveManager.instance.Data.musicVolume = level;
        SaveSystem.Save();
    }
    public void SetAmbientVolume(float level)
    {
        audioMixer.SetFloat("AmbVolume", Mathf.Log10(level) * 20f);
        AudioManager.Instance.ambSource.volume = level;
        AudioSaveManager.instance.Data.ambVolume = level;
        SaveSystem.Save();
    }
    public void SetPauseGame(bool value)
    {
        pauseMenu.SetActive(value);
        
        GamePaused = value;

        SetCursorState(cursorLocked);

    }




    public void TogglePauseOnPopup(bool value)
    {
        GamePaused = value;

    }
    public void ToggleSettings(bool value)
    {
        pausePanel.SetActive(!value);
        settingsMenu.SetActive(value);
    }

    public void ExitToMenu()
    {

        SceneManager.LoadSceneAsync("Main Menu");

    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void SetCursorState2(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.Locked;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorOpen);
       SetCursorState2(cursorLocked);
    }


}
