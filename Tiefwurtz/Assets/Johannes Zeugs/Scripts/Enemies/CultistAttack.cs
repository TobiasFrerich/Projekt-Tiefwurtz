using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{

    public class CultistAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackSpeed;

        [SerializeField] private AudioSource EnemyAttackSound;

        private EnemyMovement enemyMov;
        private Enemy enemyScr;
        private Rigidbody2D cultistBody;
        private GameObject Player;
        private bool inRange;
        private bool stayInRanged;
        private bool isShooting;
        private float timer = 10f;
        public GameObject shot;
        public Transform shotTransform;

        private void Start()
        {
            enemyMov = GetComponent<EnemyMovement>();
            enemyScr = GetComponent<Enemy>();
            Player = GameObject.Find("Player");
            cultistBody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
                return;

            if (enemyScr.Dead)
                return;

            CheckIfPlayerInRange();

            if (!stayInRanged)
            {
                timer += Time.deltaTime;
                if (timer > attackSpeed)
                {
                    stayInRanged = true;
                    timer = 0;
                }
            }
        }


        private void CheckIfPlayerInRange()
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                inRange = false;
                return; 
            }

            if (Vector2.Distance(Player.transform.position, this.transform.position) < attackRange && stayInRanged)
            {
                if (cultistBody.position.x > Player.transform.position.x)
                {
                    gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0f);
                }
                inRange = true;
                StartCoroutine(Attack());
            }
            else
            {
                inRange = false;
            }
        }
        private IEnumerator Attack()
        {
            if (!isShooting)
            {
                enemyMov.doesAttack = true;
                isShooting = true;
                cultistBody.constraints = RigidbodyConstraints2D.FreezeAll;
                if (cultistBody.position.x > Player.transform.position.x)
                {
                    gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0f);
                }
                Animator enemyAnim = GetComponent<Animator>();
                enemyAnim.SetTrigger("isAttacking");
                yield return new WaitForSeconds(0.4f);
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    EnemyAttackSound.Play();
                    Shot();
                    yield return new WaitUntil(() => (Vector2.Distance(Player.transform.position, this.transform.position) > attackRange));
                    enemyMov.doesAttack = false;
                    cultistBody.constraints = RigidbodyConstraints2D.None;
                    cultistBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
        private void Shot()
        {
            if (!inRange)
                return;

            if (stayInRanged)
            {
                Instantiate(shot, shotTransform.position, Quaternion.identity);
                stayInRanged = false;
                isShooting = false;
            }
        }
    }
}
