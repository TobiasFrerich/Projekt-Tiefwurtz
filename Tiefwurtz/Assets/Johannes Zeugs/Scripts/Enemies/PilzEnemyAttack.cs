using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Tiefwurtz
{
    public class PilzEnemyAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float hammerTimeRange = 2f;
        [SerializeField] private float hammerSpeed = 5f;
        [SerializeField] private float shakeIntesity;
        [SerializeField] private float hammerDMG;

        public CinemachineVirtualCamera CinemachineVC;

        private Rigidbody2D pilzBody;
        private Flashlight flashLight;
        private GameManagerScribt gameManager;
        private GameObject GameManager;
        private GameObject Player;
        private EnemyMovement enemyMove;
        private Enemy enemyScr;

        private bool hammerRange;
        private float canHammer = 1f;
        private bool inRange;

        private void Start()
        {
            enemyScr = GetComponent<Enemy>();
            enemyMove = GetComponent<EnemyMovement>();
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            Player = GameObject.Find("Player");
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            pilzBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (enemyScr.Dead)
            {
                pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                return;
            }

            if (gameManager.playerIsDead)
                return;

            CheckIfPlayerInRange();
            CheckIfHammerRange();
        }
        private void CheckIfHammerRange()
        {
            if (Vector2.Distance(Player.transform.position, transform.position) < hammerTimeRange)
            {
                hammerRange = true;
            }
            else
                hammerRange = false;
        }
        private void CheckIfPlayerInRange()
        {
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
            Animator enemyAnim = GetComponent<Animator>();
            if (!gameManager.playerIsDead && !enemyScr.Dead)
            {
                enemyMove.doesAttack = true;
                if (inRange)
                {
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                    if (pilzBody.position.x > Player.transform.position.x)
                    {
                        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0f);
                    }
                    else
                    {
                        gameObject.transform.localScale = new Vector3(-0.1f, 0.1f, 0f);
                    }
                    yield return new WaitForSeconds(1f);

                    //pilzBody.constraints = RigidbodyConstraints2D.None;
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                    if (!hammerRange && canHammer == 1f)
                    {
                        if (!gameManager.playerIsDead && !enemyScr.Dead)
                        {
                            Vector3 direction = Player.transform.position - transform.position;
                            Vector3 rotation = transform.position - Player.transform.position;
                            pilzBody.velocity = new Vector2(direction.x, direction.y).normalized * hammerSpeed;
                        }
                    }
                    else
                    {
                        pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                    }

                    yield return new WaitUntil(() => hammerRange);

                    if (canHammer == 1f)
                    {
                        enemyAnim.SetBool("isHammering", true);
                        yield return new WaitForSeconds(1f);
                        pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                        if (!enemyScr.Dead)
                        {
                            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                            CinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntesity;
                        }

                        canHammer = 0f;
                        yield return new WaitForSeconds(0.5f);
                        if (hammerRange)
                        {
                            if (!gameManager.playerIsDead && !enemyScr.Dead)
                            {
                                flashLight = Player.GetComponent<Flashlight>();
                                flashLight.backLight.intensity = flashLight.backLight.intensity - hammerDMG;
                                flashLight.playerLight.intensity = flashLight.playerLight.intensity - hammerDMG * 4f;
                            }
                        }
                    }

                    yield return new WaitUntil(() => canHammer == 0);


                    yield return new WaitForSeconds(0.5f);
                    enemyAnim.SetBool("isHammering", false);
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinZERO =
                    CinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlinZERO.m_AmplitudeGain = 0;
                    //pilzBody.constraints = RigidbodyConstraints2D.None;
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    yield return new WaitForSeconds(2f);
                    enemyMove.doesAttack = false;
                    canHammer = 1f;

                    yield return new WaitUntil(() => inRange == false);
                    if (canHammer == 1f)
                    {
                        //pilzBody.constraints = RigidbodyConstraints2D.None;
                        pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    if (pilzBody.velocity.x > 0)
                    {
                        gameObject.transform.localScale = new Vector3(-0.1f, 0.1f, 0f);
                    }
                    if (pilzBody.velocity.x < 0)
                    {
                        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0f);
                    }

                }
            }
        }
    }
}
