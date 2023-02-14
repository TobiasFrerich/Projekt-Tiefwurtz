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
        private GameObject player;
        private PlayerLight playerLight;

        private Cinemachine.CinemachineVirtualCamera vcam;

        public void OnDeath()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            vcam = Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            camTransform.transform.position = player.transform.position;
            vcam.Follow = camTransform.transform;
            player.SetActive(false);
            camTransform.SetActive(true);
            SetPlayerIsDead();
            StartCoroutine(Respawn());
        }
        private IEnumerator Respawn()
        {
            SetPlayerIsAlive();
            playerLight = player.GetComponent<PlayerLight>();
            yield return new WaitForSeconds(2f);

            vcam = Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            vcam.Follow = player.transform;
            PlayerLight.backLightIntensity = playerLight.startBackIntensity;
            player.transform.position = playerLight.currentSavePoint;
            camTransform.SetActive(false);
            player.SetActive(true);
        }

        public void SetPlayerIsDead()
        {
            playerIsDead = true;
        }
        public void SetPlayerIsAlive()
        {
            playerIsDead = false;
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
