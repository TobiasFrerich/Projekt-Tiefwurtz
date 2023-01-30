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
        [SerializeField] private float wieStarkErSinkenSoll;

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
        private float startingYPos;
        private float startingIntensity;

        private void Start()
        {
            startingIntensity = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().intensity;
            startingYPos = transform.position.y;
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
            {
                hammerRange = false;
            }
        }
        private void CheckIfPlayerInRange()
        {
            if (Vector2.Distance(Player.transform.position, transform.position) < attackRange && Player.transform.position.y - transform.position.y < 1.5f)
            {
                pilzBody.gravityScale = 1f;
                GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
                inRange = true;
                StartCoroutine(Attack());
            }
            else
            {
                inRange = false;
                Hide();
            }
        }

        private void Hide()
        {
            pilzBody.gravityScale = 0f;
            if(GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().intensity > 0f)
            {
                GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().intensity -= 0.01f;
            }
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            if (transform.position.y > startingYPos - wieStarkErSinkenSoll)
            {
                pilzBody.velocity = new Vector2(0f, -0.5f);
            }
            else
                pilzBody.velocity = new Vector2(0f, 0f);
            //pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private IEnumerator Attack()
        {
            if (GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().intensity < startingIntensity)
            {
                GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().intensity += 0.1f;
            }
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<EnemyMovement>().enabled = true;
            Animator enemyAnim = GetComponent<Animator>();
            if (!gameManager.playerIsDead && !enemyScr.Dead)
            {
                enemyMove.doesAttack = true;
                if (inRange)
                {
                    if (transform.position.y < startingYPos)
                        pilzBody.velocity = new Vector2(0f, 1.5f);

                    yield return new WaitUntil(() => transform.position.y >= startingYPos);

                    //pilzBody.constraints = RigidbodyConstraints2D.None;
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                    if (!hammerRange && canHammer == 1f)
                    {
                        if (!gameManager.playerIsDead && !enemyScr.Dead)
                        {
                            enemyAnim.SetBool("pilzIsRunning", true);
                            Vector3 direction = Player.transform.position - transform.position;
                            Vector3 rotation = transform.position - Player.transform.position;
                            pilzBody.velocity = new Vector2(direction.x, 0f).normalized * hammerSpeed;
                        }
                    }
                    else
                    {
                        pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                    }

                    yield return new WaitUntil(() => hammerRange);

                    if (canHammer == 1f)
                    {
                        enemyAnim.SetBool("pilzIsRunning", false);
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


                    yield return new WaitForSeconds(1f);
                    enemyAnim.SetBool("isHammering", false);
                    enemyAnim.SetBool("pilzIsRunning", true);
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
