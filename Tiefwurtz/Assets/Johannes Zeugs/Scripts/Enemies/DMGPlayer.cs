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
        private float timer;
        private Rigidbody2D spikesBody;
        private void Awake()
        {
            spikesBody = GetComponent<Rigidbody2D>();
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            Player = GameObject.Find("Player");
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            spikesBody.velocity = new Vector2(0.01f, 0f);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            timer += 1 * Time.deltaTime;
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
