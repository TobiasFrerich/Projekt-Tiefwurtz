using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Tiefwurtz
{
    public class newPilzAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float pilzAttackRange = 0.5f;
        [SerializeField] private float hammerTimeRange = 2f;
        [SerializeField] private float hammerSpeed = 5f;
        [SerializeField] private float shakeIntesity;
        [SerializeField] private float hammerDMG;
        [SerializeField] private float wieStarkErSinkenSoll;

        [SerializeField] private AudioSource AttackSound;

        public CinemachineVirtualCamera CinemachineVC;
        public GameObject spikesSpR;
        public GameObject spikesSpL;

        public Transform pilzTransform;
        public Transform leftMax;
        public Transform rightMax;

        private Vector3 currentPlayerPos;
        private Vector3 StartingPosition;
        private Rigidbody2D pilzBody;
        private PlayerLight flashLight;
        private GameManagerScribt gameManager;
        private GameObject GameManager;
        private GameObject Player;
        private EnemyMovement enemyMove;
        private Enemy enemyScr;

        private bool hidden;
        private float timer = 0f;
        private bool hammerRange;
        private bool isHammering = false;
        private bool inRange;
        private float startingYPos;
        private float startingIntensity;
        private bool savedPlayerPos;

        private float rightStartX;
        private float leftStartX;
        private Animator enemyAnim;

        private void Awake()
        {
            StartingPosition = pilzTransform.position;
        }
        private void Start()
        {
            enemyAnim = GetComponent<Animator>();
            rightStartX = rightMax.position.x;
            leftStartX = leftMax.position.x;
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
            // Debug.Log(leftStartX);
            
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
        private void CheckIfPlayerInRange()
        {
            if (Vector2.Distance(Player.transform.position, pilzTransform.position) < attackRange && Player.transform.position.y - pilzTransform.position.y < 1.5f)
            {
                StartCoroutine(Attack());
            }
            else
            {
                ReturnToStartingPosition();
                
            }
        }
        private void CheckIfHammerRange()
        {
            if (isHammering)
                return;

            if (hidden)
                return;

            if (gameManager.playerIsDead)
                return;

            if (Vector2.Distance(Player.transform.position, pilzTransform.position) < hammerTimeRange)
            {
                hammerRange = true;
            }
            else
            {
                hammerRange = false;
            }
        }

        private void WalkTowardsPlayer()
        {
            if (hidden)
                return;

            if (isHammering)
                return;

            if (gameManager.playerIsDead)
                return;

            if (pilzTransform.position.x > leftStartX && pilzTransform.position.x < rightStartX && !hammerRange)
            {
                pilzBody.constraints = RigidbodyConstraints2D.None;
                pilzBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                enemyAnim.SetBool("pilzIsRunning", true);
                Vector3 direction = Player.transform.position - pilzTransform.position;
                pilzBody.velocity = new Vector2(direction.x, 0f).normalized * hammerSpeed;
            }
        }

        private void ReturnToStartingPosition()
        {

            if (isHammering)
                return;

            pilzBody.constraints = RigidbodyConstraints2D.None;
            pilzBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            pilzBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (Vector2.Distance(StartingPosition, pilzTransform.position) < 1f)
            {
                enemyAnim.SetBool("pilzIsRunning", false);
                return;
            }
            else
            {
                Vector3 direction = StartingPosition - pilzTransform.position;
                pilzBody.velocity = new Vector2(direction.x, 0f).normalized * hammerSpeed;
            }

        }

        private Vector3 GetPlayerPosition()
        {
            if (!gameManager.playerIsDead)
            {
                savedPlayerPos = true;
                return Player.transform.position;
            }
            else
                return StartingPosition;
        }

        private void Hide()
        {
            if (isHammering)
                return;

            enemyAnim.SetTrigger("hide");
            //Play Hide Animation

        }

        private IEnumerator Attack()
        {
            enemyAnim.SetTrigger("standUP");
            yield return new WaitForSeconds(2.1f);

            WalkTowardsPlayer();

            if (!isHammering && hammerRange)
            {
                isHammering = true;

                if (!savedPlayerPos)
                    currentPlayerPos = GetPlayerPosition();

                Vector3 direction = currentPlayerPos - pilzTransform.position;
                pilzBody.velocity = new Vector2(direction.x, 0f).normalized * hammerSpeed;


                yield return new WaitUntil(() => (Vector2.Distance(currentPlayerPos, pilzTransform.position) < 1.5f));

                pilzBody.constraints = RigidbodyConstraints2D.FreezeAll;
                enemyAnim.SetBool("pilzIsRunning", false);

                enemyAnim.SetTrigger("isHammering");


                yield return new WaitForSeconds(1f);
                if (!enemyScr.Dead)
                {
                    AttackSound.Play();

                    spikesSpL.SetActive(true);
                    spikesSpR.SetActive(true);
                    /*
                    spikesSpR.GetComponent<SpriteRenderer>().enabled = true;
                    spikesSpL.GetComponent<SpriteRenderer>().enabled = true;

                    spikesSpR.GetComponent<BoxCollider2D>().enabled = true;
                    spikesSpL.GetComponent<BoxCollider2D>().enabled = true;*/
                }

                yield return new WaitForSeconds(1.23f);

                spikesSpL.SetActive(false);
                spikesSpR.SetActive(false);
                /*
                spikesSpR.GetComponent<SpriteRenderer>().enabled = false;
                spikesSpL.GetComponent<SpriteRenderer>().enabled = false;

                spikesSpR.GetComponent<BoxCollider2D>().enabled = false;
                spikesSpL.GetComponent<BoxCollider2D>().enabled = false;*/
                isHammering = false;
                savedPlayerPos = false;
            }

            if (!hammerRange && !isHammering)
            {
                savedPlayerPos = false;
                yield return new WaitForSeconds(1f);
                WalkTowardsPlayer();
            }
            
        }
    }
}
