using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
