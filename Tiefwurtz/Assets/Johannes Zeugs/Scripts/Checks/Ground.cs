using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Ground : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float cayoteTime = 0.2f;
        public bool OnGround { get; private set; }
        public bool LeavedGround;
        public float cayoteTimeCounter;
        private GameObject Player;
        private Collision2D bodenCollider;
        private bool canJump = false;
        public float Friction { get; private set; }

        //private Vector2 _normal;
        //private PhysicsMaterial2D _material;
        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {  
            if (collision.gameObject.tag == "Ground")
            {
                OnGround = true;
                LeavedGround = false;
                cayoteTimeCounter = 0;
            }
            //EvaluateCollision(collision);
            //RetrieveFriction(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            bodenCollider = collision;
            if (collision.gameObject.tag == "Ground")
            {
                StartCoroutine(CheckIfGroundIsNear());
            }
            
            Friction = 0;
        }
        private IEnumerator CheckIfGroundIsNear()
        {
            yield return new WaitForSeconds(0.1f);
            if (Player.transform.position.y < bodenCollider.gameObject.transform.position.y + 1f)
            {
                OnGround = true;
                LeavedGround = false;
            }
            yield return new WaitForSeconds(0.1f);
            if (Player.transform.position.y > bodenCollider.gameObject.transform.position.y + 1f)
            {
                OnGround = false;
                LeavedGround = true;
            }
        }

        /*
        private void FixedUpdate()
        {
            if (LeavedGround)
            {
                cayoteTimeCounter = cayoteTimeCounter + Time.deltaTime;
                if (cayoteTimeCounter > cayoteTime)
                {

                    OnGround = false;

                }               
            }
        }
        private void Update()
        {
            if (LeavedGround)
                CheckIfGroundIsNear();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //EvaluateCollision(collision);
            //RetrieveFriction(collision);
        }
        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;
                OnGround |= _normal.y >= 0.9f;
            }
        }

        private void RetrieveFriction(Collision2D collision)
        {
            _material = collision.rigidbody.sharedMaterial;

            Friction = 0;

            if(_material != null)
            {
                Friction = _material.friction;
            }
        }*/
    }
}
