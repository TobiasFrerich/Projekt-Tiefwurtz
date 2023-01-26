using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
        [SerializeField] float attackRange = 0.5f;
        [SerializeField] float attackRate = 1f;

        public Sprite attackSprite;
        public Sprite normalSprite;
        public Transform attackpoint;
        public LayerMask enemyLayers;

        private Enemy enemyHealth;
        private float nextAttackTime = 0f;

        public GameObject playerShot;
        public Transform playerShotTransform;



        private void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Animator playerAnim = GetComponent<Animator>();
                    playerAnim.SetBool("isAttacking", true);
                    StartCoroutine(MeleeAttack());
                    nextAttackTime = Time.time + 1f / attackRate;
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                Animator playerAnim = GetComponent<Animator>();
                StartCoroutine(RangedAttack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        private IEnumerator MeleeAttack()
        {
            
            yield return new WaitForSeconds(0.5f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<Enemy>();
                enemyHealth.TakeDamage(20f);
                GetComponentInChildren<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
                em.enabled = true;
            }

            yield return new WaitForSeconds(0.4f);
            Animator playerAnim = GetComponent<Animator>();
            playerAnim.SetBool("isAttacking", false);
        }

        private IEnumerator RangedAttack()
        {
            Animator playerAnim = GetComponent<Animator>();
            playerAnim.SetBool("isAttacking", true);
            Instantiate(playerShot, playerShotTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool("isAttacking", false);
        }

        private void OnDrawGizmosSelected()
        {
            if (attackpoint == null)
                return;

            Gizmos.DrawWireSphere(attackpoint.position, attackRange);
        }
    }
}
