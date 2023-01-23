using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
<<<<<<< Updated upstream
        [SerializeField] float attackRange = 0.5f;
        [SerializeField] float attackRate = 1f;
=======
        [SerializeField] float meleeAttackRange = 0.5f;
        [SerializeField] float meleeAttackRate = 1f;
        [SerializeField] float meleeAttackDMG = 20f;
>>>>>>> Stashed changes

        public SpriteRenderer _spriteRenderer;
        public Sprite attackSprite;
        public Sprite normalSprite;
        public Transform attackpoint;
        public LayerMask enemyLayers;

        private Enemy enemyHealth;
        private float nextAttackTime = 0f;
        private bool canRangeAttack;

<<<<<<< Updated upstream
=======
        public GameObject rangedAttackShot;
        public Transform rangedAttackTransform;


>>>>>>> Stashed changes
        private void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(MeleeAttack());
                    nextAttackTime = Time.time + 1f / meleeAttackRate;
                }
            }
            if (canRangeAttack && Input.GetKeyDown("s"))
            {
                StartCoroutine(RangedAttack());
            }
        }

        private IEnumerator MeleeAttack()
        {
            _spriteRenderer.sprite = attackSprite;
            yield return new WaitForSeconds(0.1f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, meleeAttackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<Enemy>();
<<<<<<< Updated upstream
                enemyHealth.TakeDamage(20f);
=======
                enemyHealth.TakeDamage(meleeAttackDMG);
                GetComponentInChildren<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
                em.enabled = true;
>>>>>>> Stashed changes
            }

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.sprite = normalSprite;
        }

        private IEnumerator RangedAttack()
        {
            _spriteRenderer.sprite = attackSprite;
            yield return new WaitForSeconds(0.1f);
            Instantiate(rangedAttackShot, rangedAttackTransform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.sprite = normalSprite;
        }

        private void OnDrawGizmosSelected()
        {
            if (attackpoint == null)
                return;

            Gizmos.DrawWireSphere(attackpoint.position, meleeAttackRange);
        }
    }
}
