using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiefwurtz
{
    public class OnExitLevel : MonoBehaviour
    {
        public string sceneName;
        private GameManagerScribt gameManagerScr;
        private GameObject GameManager;
        [SerializeField] private Vector3 sceneSpawnPoint;
        private void Start()
        {
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManagerScr = GameManager.GetComponent<GameManagerScribt>();
        }
        private void OnTriggerEnter2D(Collider2D player)
        {
            if (player.tag == "Player")
            {
                PlayerLight.reachedACheckpoint = true;
                PlayerLight.currentSavePoint = sceneSpawnPoint;
                StartCoroutine(gameManagerScr.LoadLevel(sceneName));
            }
        }
    }
}
