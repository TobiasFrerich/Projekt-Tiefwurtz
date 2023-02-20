using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiefwurtz
{
    public class GameManagerScribt : MonoBehaviour
    {
        public bool playerIsDead { get; private set; }

        public GameObject Camera;
        public GameObject camTransform;
        private Animator SceneAnim;
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
            yield return new WaitForSeconds(2f);
            playerLight = player.GetComponent<PlayerLight>();
            SetPlayerIsAlive();
            camTransform.SetActive(false);
            player.SetActive(true);
            vcam = Camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            vcam.Follow = player.transform;
            PlayerLight.backLightIntensity = 10f;
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public IEnumerator LoadLevel(string sceneName)
        {
            SceneAnim = GetComponent<Animator>();
            SceneAnim.SetTrigger("Start");

            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
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
