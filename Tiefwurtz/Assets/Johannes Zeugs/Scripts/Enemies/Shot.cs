using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{

    public class Shot : MonoBehaviour
    {
        [SerializeField] private float shotDmg = 1f;
        [SerializeField] private AudioSource Hit;

        private Transform PlayerHitBox;
        private Rigidbody2D shotBody;
        private GameObject player;
        private PlayerLight flashLight;


        public float force;
        private float timer;
        private float flyTime = 5f;

        private Vector2 standing = new Vector2(0, 0);

        void Awake()
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
                return;

            shotBody = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerHitBox = player.transform.Find("hitbox");
            if (player.GetComponent<Rigidbody2D>().velocity == standing)
            {
                Vector3 direction = player.transform.position - transform.position;
                Vector3 rotation = transform.position - player.transform.position;
                shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
            }
            else
            {
                Vector3 direction = PlayerHitBox.position - transform.position;
                Vector3 rotation = transform.position - PlayerHitBox.position;
                shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > flyTime)
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                flashLight = player.GetComponent<PlayerLight>();
                flashLight.backLight.intensity = flashLight.backLight.intensity - shotDmg;
                Hit.Play();
                Destroy(gameObject);
            }
        }
        
    }
}
