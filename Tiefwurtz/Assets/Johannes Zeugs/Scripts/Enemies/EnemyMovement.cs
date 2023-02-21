using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float enemySpeed = 1f;
        [SerializeField] private float xScale = 1f;
        [SerializeField] private float yScale = 1f;

        [SerializeField] private AudioSource WalkSound;

        public bool sollStehen;
        public bool isPilzEnemy;
        public bool doesAttack = false;

        public GameObject _leftMax;
        public GameObject _rightMax;

        private float rightStartX;
        private float leftStartX;
        private PilzEnemyAttack pilzAttack;
        private Rigidbody2D enemyBody;

        private bool mussrechts = true;

        void Start()
        {
            enemyBody = GetComponent<Rigidbody2D>();
            if(!isPilzEnemy)
                enemyBody.velocity = new Vector2(enemySpeed, enemyBody.velocity.y);
            rightStartX = _rightMax.transform.position.x;
            leftStartX = _leftMax.transform.position.x;
        }

        private void FixedUpdate()
        {
            ChecktoFlipSprite();
        }

        void Update()
        {
            if (Time.timeScale == 0f)
            {
                if (!isPilzEnemy)
                {
                    WalkSound.enabled = false;
                }
                return;
            }

            if (sollStehen)
            {
                Animator enemyAnim = GetComponent<Animator>();
                enemyAnim.SetBool("isRunning", false);
                return;
            }
            if (doesAttack)
            {
                if (isPilzEnemy)
                    return;

                Animator enemyAnim = GetComponent<Animator>();
                enemyAnim.SetBool("isRunning", false);
                return;
            }

            if (enemyBody.velocity.x != 0)
            {
                if (isPilzEnemy)
                    return;

                WalkSound.enabled = true;
                Animator enemyAnim = GetComponent<Animator>();
                enemyAnim.SetBool("isRunning", true);
            }
            else
            {
                if (isPilzEnemy)
                    return;
                WalkSound.enabled = false;
                Animator enemyAnim = GetComponent<Animator>();
                enemyAnim.SetBool("isRunning", false);
            }

            if (enemyBody.position.x < leftStartX)
            {
                mussrechts = true;
            }
            else if (enemyBody.position.x > rightStartX)
            {
                mussrechts = false;
            }
            if (mussrechts)
            {
                enemyBody.velocity = new Vector2(enemySpeed, enemyBody.velocity.y);
            }
            else
            {
                enemyBody.velocity = new Vector2(-enemySpeed, enemyBody.velocity.y);
            }
        }
        private void ChecktoFlipSprite()
        {
            if (enemyBody.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-xScale, yScale, 0f);
            }
            if (enemyBody.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(xScale, yScale, 0f);
            }
        }
    }
}

