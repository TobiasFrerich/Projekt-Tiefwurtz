using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float enemyHealth = 100f;
        [SerializeField] private AudioSource Hit;

        [SerializeField] private ParticleSystem DeadParticals;

        private EnemyMovement enemyMovement;
        public GameObject item;
        public Transform itemTransform;
        public Color _color;
        public bool Dead = false;

        private void Start()
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        private void Update()
        {
            if (enemyHealth < 1f)
            {
                OnDeath();
            }
        }

        private void Hurt()
        {
            Animator enemyAnim = GetComponent<Animator>();
            enemyAnim.SetTrigger("isHit");
            Hit.Play();
        }

        private void OnDeath()
        {
            if (Dead == true)
                return;

            DeadParticals.Play();
            ParticleSystem.EmissionModule em = DeadParticals.emission;
            em.enabled = true;

            Animator enemyAnim = GetComponent<Animator>();
            enemyAnim.SetTrigger("isDead");
            GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Instantiate(item, itemTransform.position, Quaternion.identity);
            Dead = true;
        }

        public void TakeDamage(float dmg)
        {
            if (enemyHealth > 1f)
            {
                Hurt();
            }
            enemyHealth = enemyHealth - dmg;
        }
    }
}
