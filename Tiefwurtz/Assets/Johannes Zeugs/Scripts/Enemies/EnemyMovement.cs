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

        public bool sollStehen;
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
            if (sollStehen)
            {
                return;
            }
            if (doesAttack)
                return;

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

