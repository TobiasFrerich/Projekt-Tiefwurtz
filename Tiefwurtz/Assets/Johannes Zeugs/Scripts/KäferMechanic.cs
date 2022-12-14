using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class KäferMechanic : MonoBehaviour
    {
        public bool aufnRücken { get; private set; }
        private float rückenCounter;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                aufnRücken = true;
                Turn();
            }
        }

        private void Turn()
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}
