using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{

    public class CultistAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackSpeed;

        private Rigidbody2D cultistBody;
        private GameObject Player;
        private bool inRange;
        private bool stayInRanged;
        private bool playerIsAlive = true;
        private float timer;
        public GameObject shot;
        public Transform shotTransform;

        private void Start()
        {
            Player = GameObject.Find("Player");
            cultistBody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            CheckIfPlayerInRange();

            Shot();
        }

        public void CheckIfPlayerIsAlive()
        {
            playerIsAlive = false;
            inRange = false;
        }

        private void CheckIfPlayerInRange()
        {
            if (!playerIsAlive)
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
            cultistBody.constraints = RigidbodyConstraints2D.FreezeAll;
            if (cultistBody.position.x > Player.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
            }
            yield return new WaitUntil(() => inRange == false);
            cultistBody.constraints = RigidbodyConstraints2D.None;
            cultistBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (cultistBody.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
            }
            if (cultistBody.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
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
