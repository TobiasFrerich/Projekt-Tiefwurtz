using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float enemySpeed = 1f;

        public GameObject _leftMax;
        public GameObject _rightMax;

        private float rightStartX;
        private float leftStartX;

        private Rigidbody2D enemyBody;
        private KäferMechanic käfMec;

        private bool mussrechts = true;
        private bool aufnRücken = false;

        void Start()
        {
            enemyBody = GetComponent<Rigidbody2D>();
            käfMec = GetComponent<KäferMechanic>();
            enemyBody.velocity = new Vector2(enemySpeed, enemyBody.velocity.y);
            rightStartX = _rightMax.transform.position.x;
            leftStartX = _leftMax.transform.position.x;
        }

        private void FixedUpdate()
        {
            ChecktoFlipSprite();

            aufnRücken = käfMec.aufnRücken;
        }

        void Update()
        {
            if (aufnRücken)
            {
                return;
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
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
            }
            if (enemyBody.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
            }
        }
    }
}

