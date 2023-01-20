using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Tiefwurtz
{
    public class PilzEnemyAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float pilzDMG;
        [SerializeField] private float hammerTimeRange = 2f;
        [SerializeField] private float hammerSpeed = 5f;
        [SerializeField] private float shakeIntesity;

        public CinemachineVirtualCamera CinemachineVC;
        private Rigidbody2D pilzBody;
        private GameManagerScribt gameManager;
        private GameObject GameManager;
        private GameObject Player;
        private EnemyMovement enemyMove;
        private Flashlight flashLight;
        private bool hammerRange;
        private float canHammer = 1f;
        private bool inRange;
        private bool playerIsDead = false;

        private void Start()
        {
            enemyMove = GetComponent<EnemyMovement>();
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            Player = GameObject.Find("Player");
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            pilzBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckIfPlayerInRange();
            CheckIfHammerRange();
        }
        private void CheckIfHammerRange()
        {
            if (playerIsDead)
                return;

            if (transform.position.x - Player.transform.position.x > -hammerTimeRange && transform.position.x - Player.transform.position.x < hammerTimeRange)
            {
                hammerRange = true;
            }
            else
                hammerRange = false;
        }
        private void CheckIfPlayerInRange()
        {
            playerIsDead = gameManager.playerIsDead;

            if (playerIsDead)
                return;

            if (Player.transform.position.x - gameObject.transform.position.x < attackRange && Player.transform.position.x - gameObject.transform.position.x > -attackRange
                && Player.transform.position.y - gameObject.transform.position.y < attackRange)
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
            enemyMove.doesAttack = true;
            if (inRange)
            {
                pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                if (pilzBody.position.x > Player.transform.position.x)
                {
                    gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
                }
                yield return new WaitForSeconds(1f);

                //pilzBody.constraints = RigidbodyConstraints2D.None;
                pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                if (!hammerRange && canHammer == 1f)
                {
                    Vector3 direction = Player.transform.position - transform.position;
                    Vector3 rotation = transform.position - Player.transform.position;
                    pilzBody.velocity = new Vector2(direction.x, direction.y).normalized * hammerSpeed;
                }
                else
                {
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                }

                yield return new WaitUntil(() => hammerRange);

                if (canHammer == 1f)
                {
                    if (Player.transform.position.x < transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(Vector3.forward * 90f);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(Vector3.forward * 270f);
                    }
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    CinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntesity;
                    canHammer = 0f;
                    
                }


                yield return new WaitUntil(() => canHammer == 0);

                yield return new WaitForSeconds(0.5f);
                if(hammerRange)
                {
                    if (!playerIsDead)
                    {
                        flashLight = Player.GetComponent<Flashlight>();
                        flashLight.backLight.intensity = flashLight.backLight.intensity - pilzDMG;
                        flashLight.playerLight.intensity = flashLight.playerLight.intensity - pilzDMG * 4f;
                    }
                }
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinZERO =
                CinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlinZERO.m_AmplitudeGain = 0f;
                transform.rotation = Quaternion.Euler(Vector3.forward * 0f);
                //pilzBody.constraints = RigidbodyConstraints2D.None;
                pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                yield return new WaitForSeconds(5f);
                enemyMove.doesAttack = false;
                canHammer = 1f;

                yield return new WaitUntil(() => inRange == false);
                if(canHammer == 1f)
                {
                    //pilzBody.constraints = RigidbodyConstraints2D.None;
                    pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }

                if (pilzBody.velocity.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
                }
                if (pilzBody.velocity.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
                }
            }
            
        }
    }
}
