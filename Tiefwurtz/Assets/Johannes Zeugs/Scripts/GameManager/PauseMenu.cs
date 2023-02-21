using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Tiefwurtz
{

    public class PauseMenu : MonoBehaviour
    {
        
        public static bool GameIsPaused = false;
        public GameObject PauseMenuUI;
        public AudioMixer AudMix;
        const string MIXER_MASTER = "MasterVolume";

        public void SetVolume(float volume)
        {
            AudMix.SetFloat("MasterVolume", volume);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
            GameIsPaused = false;
        }

        void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        
    }
}
