using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiefwurtz
{
    public class OnExitLevel : MonoBehaviour
    {
        public string sceneName;
        public bool isNextScene = true;

        [SerializeField] public SceneInfo sceneInfo;
        [SerializeField] private Vector3 sceneSpawnPoint;

        private void OnTriggerEnter2D(Collider2D player)
        {
            if (player.tag == "Player")
            {
                sceneInfo.isNextScene = isNextScene;
                PlayerLight.reachedACheckpoint = true;
                PlayerLight.currentSavePoint = sceneSpawnPoint;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }
}
