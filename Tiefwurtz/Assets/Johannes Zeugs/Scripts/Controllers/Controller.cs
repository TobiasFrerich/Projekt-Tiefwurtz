using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tiefwurtz
{
    public class Controller : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(0);
            }
        }
        public InputController input = null;
    }
}
