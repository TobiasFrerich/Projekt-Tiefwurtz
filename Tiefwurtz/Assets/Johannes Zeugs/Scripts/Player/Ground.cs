using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Ground : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float cayoteTime = 0.2f;
        

        private Animator playerAnim;
        public bool OnGround { get; private set; }
        public bool LeavedGround;
        public float cayoteTimeCounter;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {  
            if (collision.gameObject.tag == "Ground")
            {
                OnGround = true;
                LeavedGround = false;
                cayoteTimeCounter = 0;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                //OnGround = false;
                LeavedGround = true;

                playerAnim = GetComponent<Animator>();
                playerAnim.SetBool("isJumping", true);
            }
        }

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
    }
}
