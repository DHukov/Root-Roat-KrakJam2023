using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuState { Play, Pause, }

public class PauseMenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public MenuState menuState;

    public Slider Slider;
    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);

        Slider.value = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuState == MenuState.Play)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && menuState == MenuState.Pause)
            Resume();
    }

    public void Resume()
    {
        menuState = MenuState.Play;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        menuState = MenuState.Pause;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }

    public void ChangeVolume(float newVolume)
    {
        newVolume = Slider.value;
        PlayerPrefs.SetFloat("volume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
