using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{

    public class PlayerShot : MonoBehaviour
    {
        [SerializeField] private float shotDmg = 1f;
        [SerializeField] private float flyTime = 5f;
        [SerializeField] float force = 1f;

        private GameObject Player;
        private Rigidbody2D shotBody;
        private Enemy enemyHealth;
        private float timer;

        void Awake()
        {
            shotBody = GetComponent<Rigidbody2D>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            Vector3 rotation = transform.position - mousePosition;
            shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
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
            Player = GameObject.FindGameObjectWithTag("Player");
            if (collision.gameObject == Player)
                return;


            enemyHealth = collision.gameObject.GetComponent<Enemy>();
            enemyHealth.TakeDamage(shotDmg);
            Destroy(gameObject);
        }
        
    }
}
