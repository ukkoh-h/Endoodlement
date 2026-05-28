using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private string gameScene;
    

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambientSlider;

    private void Awake()
    {

    }

    private void Start()
    {
        var data = AudioSaveManager.instance.Data;

        masterSlider.value = data.masterVolume;
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
        ambientSlider.value = data.ambVolume;
    }

    public void StartGame()
    {
         SceneManager.LoadSceneAsync(gameScene);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
