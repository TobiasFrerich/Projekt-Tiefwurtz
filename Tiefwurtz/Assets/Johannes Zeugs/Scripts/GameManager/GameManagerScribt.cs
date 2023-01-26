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

        private SpriteRenderer _spriteRenderer;
        private GameObject player;
        private Cinemachine.CinemachineVirtualCamera vcam;

        public void OnDeath()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            vcam = Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            camTransform.transform.position = player.transform.position;
            vcam.Follow = camTransform.transform;
            _spriteRenderer = camTransform.GetComponent<SpriteRenderer>();
            Destroy(player);
            camTransform.SetActive(true);
            _spriteRenderer.sprite = DeadPlayer;
            SetPlayerIsDead();
            //Time.timeScale = 0;
        }

        public void SetPlayerIsDead()
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
