using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Tiefwurtz
{
    public class mainMenu : MonoBehaviour
    {
        public static bool SpeedRunMode;
        private void Start()
        {
            PauseMenu.currentTime = 0;
            RangedAttackItemUI.RATextPlayed = false;
            DashItemUI.DashTextPlayed = false;
            Move.dashUnlocked = false;
            PlayerLight.reachedACheckpoint = false;
            PlayerLight.currentSavePoint = new Vector3(-4f, 27f, 0f);
            PlayerLight.backLightIntensity = 10f;
            Playerattack.rangedAttackUnlocked = false;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }
        public void SetSpeedRunMode(bool value)
        {
            if (value == true)
            {
                SpeedRunMode = true;
            }
            else
            {
                SpeedRunMode = false;
            }
        }
    }
}
