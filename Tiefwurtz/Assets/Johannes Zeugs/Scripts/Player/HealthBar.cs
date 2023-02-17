using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tiefwurtz
{
    public class HealthBar : MonoBehaviour
    {
        private Image currenthealthBar;
        private GameObject Player;
        private PlayerLight playerLight;

        private void Start()
        {
            currenthealthBar = GetComponent<Image>();
            Player = GameObject.FindGameObjectWithTag("Player");
            playerLight = Player.GetComponent<PlayerLight>();
        }

        private void Update()
        {
            currenthealthBar.fillAmount = PlayerLight.backLightIntensity / playerLight.maxBackLight;
        }
    }
}
