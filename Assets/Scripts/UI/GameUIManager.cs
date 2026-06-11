using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameUIManager : MonoBehaviour
{

    public static GameUIManager instance;
    public MonoBehaviour FirstPersonController;
    public MonoBehaviour Gun;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambientSlider;


    [SerializeField] private AudioMixer audioMixer;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject pausePanel;
    public GameObject Controls;


    public bool cursorInputForLook = false;
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

    private void Start()
    {

        var data = AudioSaveManager.instance.Data;

        masterSlider.value = data.masterVolume;
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
        ambientSlider.value = data.ambVolume;

        AudioManager.Instance.PlayMusic("Forest");

    }
    private void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
           if(pauseMenu.activeSelf)
            {
                ResumeGame();
               
            }
           else
            {
                PauseGame();
            }
           
        }
    }

    public void PauseGame()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        FirstPersonController.enabled = false;
        Gun.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void DeathPause()
    {
        Time.timeScale = 0f;
        FirstPersonController.enabled = false;
        Gun.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void ResumeGame()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        FirstPersonController.enabled = true;
        Gun.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void DeathResume()
    {
        Time.timeScale = 1f;
        FirstPersonController.enabled = true;
        Gun.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    




    public void TogglePauseOnPopup(bool value)
    {
        GamePaused = value;

    }
    public void ToggleSettings(bool value)
    {
        pausePanel.SetActive(!value);
        settingsMenu.SetActive(value);
    }


    public void ToggleControls(bool value)
    {
        settingsMenu.SetActive(!value);
        Controls.SetActive(value);
    }


    public void ExitToMenu()
    {

        SceneManager.LoadSceneAsync("Main Menu");

    }

  
   

}
