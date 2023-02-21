using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
using TMPro;

namespace Tiefwurtz
{

    public class PauseMenu : MonoBehaviour
    {
        public GameObject Timer;
        public static float currentTime;
        public TextMeshProUGUI currentTimeText;
        

        public static bool GameIsPaused = false;
        public GameObject PauseMenuUI;
        public AudioMixer AudMix;
        const string MIXER_MASTER = "MasterVolume";

        private void Update()
        {
            if(mainMenu.SpeedRunMode)
            {
                Timer.SetActive(true);
                SpeedRun();
            }
            else
            {
                Timer.SetActive(false);
            }

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
        public void SetVolume(float volume)
        {
            AudMix.SetFloat("MasterVolume", volume);
        }

        void SpeedRun()
        {
            currentTime = currentTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        }

        public void Resume()
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
            SceneManager.LoadScene(1);
        }
        
    }
}
