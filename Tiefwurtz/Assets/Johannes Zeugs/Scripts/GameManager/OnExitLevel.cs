using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnExitLevel : MonoBehaviour
{
    public string sceneName;
    public bool isNextScene = true;

    [SerializeField] public SceneInfo sceneInfo;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            sceneInfo.isNextScene = isNextScene;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
