using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiefwurtz
{
    public class restartGame : MonoBehaviour
    {
        public PlayerLight pl;
        public Light2D backLight;
        public Light2D playerLight;
        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                backLight.intensity = pl.startBackIntensity;
                playerLight.intensity = pl.startPlayerIntensity;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
