using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tiefwurtz
{
    public class GameManagerScribt : MonoBehaviour
    {
        public bool playerIsDead { get; private set; }

        public GameObject Camera;
        public GameObject camTransform;
        public Sprite DeadPlayer;

        private CultistAttack cultAttack;
        private SpriteRenderer _spriteRenderer;
        private GameObject player;
        private Cinemachine.CinemachineVirtualCamera vcam;
        private Rigidbody2D deadPlayerBody;

        public void OnDeath(GameObject enemy)
        {
            cultAttack = enemy.GetComponent<CultistAttack>();
            player = GameObject.FindGameObjectWithTag("Player");
            vcam = Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            camTransform.transform.position = player.transform.position;
            deadPlayerBody = camTransform.GetComponent<Rigidbody2D>();
            vcam.Follow = camTransform.transform;
            _spriteRenderer = camTransform.GetComponent<SpriteRenderer>();
            Destroy(player);
            camTransform.SetActive(true);
            _spriteRenderer.sprite = DeadPlayer;
            SetPlayerIsNotDead();
            //Time.timeScale = 0;
        }

        public void SetPlayerIsNotDead()
        {
            playerIsDead = true;
        }
        public void PauseGame()
        {
            Time.timeScale = 0;
        }
        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
