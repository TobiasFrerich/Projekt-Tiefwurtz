using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public AudioMixer AudMix;
    const string MIXER_MASTER = "MasterVolume";
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudMix.SetFloat(MIXER_MASTER, 0f);
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudMix.SetFloat(MIXER_MASTER, -80f);
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        AudMix.SetFloat(MIXER_MASTER, 0f);
        SceneManager.LoadScene(0);
    }
}
