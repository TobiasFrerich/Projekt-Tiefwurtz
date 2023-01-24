using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
        [SerializeField] float attackRange = 0.5f;
        [SerializeField] float attackRate = 1f;

        public SpriteRenderer _spriteRenderer;
        public Sprite attackSprite;
        public Sprite normalSprite;
        public Transform attackpoint;
        public LayerMask enemyLayers;

        private Enemy enemyHealth;
        private float nextAttackTime = 0f;



        private void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(Attack());
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        private IEnumerator Attack()
        {
            _spriteRenderer.sprite = attackSprite;
            yield return new WaitForSeconds(0.1f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<Enemy>();
                enemyHealth.TakeDamage(20f);
                GetComponentInChildren<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
                em.enabled = true;
            }

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.sprite = normalSprite;
        }

        private void OnDrawGizmosSelected()
        {
            if (attackpoint == null)
                return;

            Gizmos.DrawWireSphere(attackpoint.position, attackRange);
        }
    }
}
