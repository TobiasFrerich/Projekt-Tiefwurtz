using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class DMGPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource Hit;

        [SerializeField] private float hammerDMG = 20f;
        private PlayerLight flashLight;
        private GameObject Player;
        private GameManagerScribt gameManager;
        private GameObject GameManager;
        private void Awake()
        {
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            Player = GameObject.Find("Player");
            gameManager = GameManager.GetComponent<GameManagerScribt>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (gameManager.playerIsDead)
                    return;
                Hit.Play();
                flashLight = Player.GetComponent<PlayerLight>();
                flashLight.backLight.intensity = flashLight.backLight.intensity - hammerDMG;
            }
        }
    }
}
