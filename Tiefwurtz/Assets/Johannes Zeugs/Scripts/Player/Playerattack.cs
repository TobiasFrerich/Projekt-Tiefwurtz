using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
        [SerializeField] float attackRange = 0.5f;
        [SerializeField] float attackRate = 1f;
        [SerializeField] float rangedAttackRate = 1f;
        [SerializeField] float playerAttackDMG = 20f;
        [SerializeField] float abilityUseDmg = 2f;

        [SerializeField] private AudioSource kratzAttack;
        [SerializeField] private AudioSource ShotSound;

        public Transform attackpoint;
        public LayerMask enemyLayers;

        private GameObject GameManager;
        private GameManagerScribt gameManagerScr;
        private PlayerLight playerLight;
        private Enemy enemyHealth;
        private float nextAttackTime = 0f;
        private float nextRangedAttackTime = 0f;
        public static bool rangedAttackUnlocked;

        public GameObject playerShot;
        public Transform playerShotTransform;

        private void Start()
        {
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManagerScr = GameManager.GetComponent<GameManagerScribt>();
        }

        private void Update()
        {
            if (gameManagerScr.playerIsDead)
                return;

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Animator playerAnim = GetComponent<Animator>();
                    playerAnim.SetBool("isAttacking", true);
                    kratzAttack.Play();
                    StartCoroutine(MeleeAttack());
                    nextAttackTime = Time.time + 1f / attackRate;
                }

            }
            if (Time.time >= nextRangedAttackTime)
            {
                if (!rangedAttackUnlocked)
                    return;

                if (Input.GetMouseButtonDown(1))
                {
                    Animator playerAnim = GetComponent<Animator>();
                    StartCoroutine(RangedAttack());
                    nextRangedAttackTime = Time.time + 1f / rangedAttackRate;
                }
            }
        }

        private IEnumerator MeleeAttack()
        {
            
            yield return new WaitForSeconds(0.1f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<Enemy>();
                enemyHealth.TakeDamage(playerAttackDMG);
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
            playerAnim.SetBool("isSpecialAttacking", true);
            ShotSound.Play();

            Instantiate(playerShot, playerShotTransform.position, Quaternion.identity);
            playerLight = GetComponent<PlayerLight>();
            playerLight.backLight.intensity = playerLight.backLight.intensity - abilityUseDmg;
            //playerLight.playerLight.intensity = playerLight.playerLight.intensity - abilityUseDmg * 0.5f;
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool("isSpecialAttacking", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject RangedAttackItem = GameObject.FindGameObjectWithTag("RangedAttackItem");
            if (collision.gameObject == RangedAttackItem)
            {
                rangedAttackUnlocked = true;
            }
        }
        private void OnDrawGizmosSelected()
        {
            if (attackpoint == null)
                return;

            Gizmos.DrawWireSphere(attackpoint.position, attackRange);
        }
    }
}
