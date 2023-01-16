using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class BeatleMovement : MonoBehaviour
    {
        [SerializeField] private float enemySpeed = 1f;

        public GameObject _leftMax;
        public GameObject _rightMax;

        private float rightStartX;
        private float leftStartX;

        private Rigidbody2D enemyBody;
        private K�ferMechanic k�fMec;

        private bool rechts = false;
        private bool aufnR�cken = false;

        void Start()
        {
            //Random Start Richtung
            int Dir = Random.Range(0, 2);
            if (Dir == 1)
            {
                rechts = true;
            }
            else
            {
                rechts = false;
            }

            enemyBody = GetComponent<Rigidbody2D>();
            k�fMec = GetComponent<K�ferMechanic>();
            rightStartX = _rightMax.transform.position.x;
            leftStartX = _leftMax.transform.position.x;
        }

        private void FixedUpdate()
        {
            ChecktoFlipSprite();

            aufnR�cken = k�fMec.aufnR�cken;
        }

        void Update()
        {
            if (aufnR�cken)
            {
                return;
            }

            if (enemyBody.position.x < leftStartX)
            {
                rechts = true;
            }
            else if (enemyBody.position.x > rightStartX)
            {
                rechts = false;
            }
            if (rechts)
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

