using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class KäferMechanic : MonoBehaviour
    {
        [SerializeField] private float turnedTimer = 3f;
        [SerializeField] private float turnCooldown = 5f;
        [SerializeField] private float käferJumpForce = 20f;
        public bool aufnRücken { get; private set; }
        private GameObject Player;
        private Rigidbody2D playerBody;

        private Rigidbody2D käferBody;
        

        private bool steht = true;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            käferBody = gameObject.GetComponent<Rigidbody2D>();
            playerBody = Player.GetComponent<Rigidbody2D>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && steht && (Player.transform.position.y > käferBody.position.y + 0.5f))
            {
                StartCoroutine(Turn());
            }
            if (collision.gameObject.tag == "Player" && aufnRücken && (Player.transform.position.y > käferBody.position.y + 0.5f))
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, käferJumpForce);
            }
        }
        private IEnumerator Turn()
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 7);
            steht = false;
            gameObject.transform.Rotate(new Vector3(0, 0, 180));
            käferBody.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(0.2f);
            aufnRücken = true;
            yield return new WaitForSeconds(turnedTimer);
            gameObject.transform.Rotate(new Vector3(0, 0, 180));
            käferBody.constraints = RigidbodyConstraints2D.None;
            käferBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            aufnRücken = false;
            steht = true;
        }
    }
}
