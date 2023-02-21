using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tiefwurtz
{
    public class finish : MonoBehaviour
    {
        public GameObject UI;
        public GameObject EndScreen;
        private bool Ende = false;
        private void Update()
        {
            if (Ende)
                Time.timeScale = 0f;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Ende = true;
                UI.SetActive(false);
                EndScreen.SetActive(true);
                Time.timeScale = 0f;
                //mainMenu.SpeedRunMode = false;
            }
        }
    }
}
