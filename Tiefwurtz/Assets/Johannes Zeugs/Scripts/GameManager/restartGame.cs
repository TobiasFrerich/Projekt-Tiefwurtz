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
        private GameObject Player;
        private PlayerLight pl;
        private Light2D backLight;
        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            pl = Player.GetComponent<PlayerLight>();
        }
        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                pl.backLight.intensity = pl.startBackIntensity;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
