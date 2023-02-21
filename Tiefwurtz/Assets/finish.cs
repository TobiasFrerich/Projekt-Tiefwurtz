using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tiefwurtz
{
    public class finish : MonoBehaviour
    {
        public GameObject EndScreen;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                EndScreen.SetActive(true);
                Time.timeScale = 0f;
                //mainMenu.SpeedRunMode = false;
            }
        }
    }
}
