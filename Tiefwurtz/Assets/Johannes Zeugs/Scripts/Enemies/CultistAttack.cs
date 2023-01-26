using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{

    public class CultistAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackSpeed;

        private Enemy enemyScr;
        private Rigidbody2D cultistBody;
        private GameObject Player;
        private GameObject GameManager;
        private GameManagerScribt gameManager;
        private bool inRange;
        private bool stayInRanged;
        private bool playerIsDead = false;
        private float timer = 10f;
        public GameObject shot;
        public Transform shotTransform;

        private void Start()
        {
            enemyScr = GetComponent<Enemy>();
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            Player = GameObject.Find("Player");
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            cultistBody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (enemyScr.Dead)
                return;

            CheckIfPlayerInRange();

            //Shot();
        }


        private void CheckIfPlayerInRange()
        {
            playerIsDead = gameManager.playerIsDead;

            if (playerIsDead)
            {
                inRange = false;
                return; 
            }

            if (Vector2.Distance(Player.transform.position, transform.position) < attackRange)
            {

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
            enemyAnim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.3f);
            Shot();
            yield return new WaitUntil(() => inRange == false);
            enemyAnim.SetBool("isAttacking", false);
             
            cultistBody.constraints = RigidbodyConstraints2D.None;
            cultistBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (cultistBody.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0f);
            }
            if (cultistBody.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
            }
        }
        private void Shot()
        {
            if (!inRange)
                return;

            if (!stayInRanged)
            {
                timer += Time.deltaTime;
                if (timer > attackSpeed)
                {
                    stayInRanged = true;
                    timer = 0;
                }
            }

            if (stayInRanged)
            {
                Instantiate(shot, shotTransform.position, Quaternion.identity);
                stayInRanged = false;
            }
        }
    }
}
