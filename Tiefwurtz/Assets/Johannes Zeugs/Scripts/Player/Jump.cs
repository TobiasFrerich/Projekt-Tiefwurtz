using UnityEngine;

namespace Tiefwurtz
{
    [RequireComponent(typeof(Controller))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0f, 10f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
        [SerializeField, Range(0f, 20f)] private float maxFallSpeed = 10f;
        [SerializeField, Range(0f, 1f)] private float cayoteTime = 0.2f;

        public Transform groundCheckCollider;
        public float groundCheckRange;
        public LayerMask goundLayers;
        public float cayoteTimeCounter;

        private Controller controller;
        private Rigidbody2D body;
        private Ground ground;
        private Move move;
        private Vector2 velocity;
        private Animator playerAnim;

        private int jumpPhase;
        private float defaultGravityScale;
        private float jumpSpeed;

        public bool currentlyJumping;
        private bool desiredJump;
        private bool onGround;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();
            move = GetComponent<Move>();
            playerAnim = GetComponent<Animator>();

            defaultGravityScale = 1f;
        }

        void Update()
        {
            desiredJump |= controller.input.RetrieveJumpInput();
        }

        private void FixedUpdate()
        {
            GroundCheck();

            //onGround = ground.OnGround;
            velocity = body.velocity;

            if (onGround)
            {
                jumpPhase = 0;
            }

            if (desiredJump)
            {
                JumpAction();
                desiredJump = false;
            }

            if (move.isDashing)
            {
                return;
            }

            if (body.velocity.y > 0)
            {
                body.gravityScale = upwardMovementMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = downwardMovementMultiplier;
            }
            else if(body.velocity.y == 0)
            {
                body.gravityScale = defaultGravityScale;
                if (onGround)
                {
                    currentlyJumping = false;
                }
            }
            body.velocity = velocity;
            
            if (body.velocity.y < (maxFallSpeed * -1))
            {
                body.velocity = new Vector2(velocity.x, (maxFallSpeed * -1));
            }
        }
        private void GroundCheck()
        {
            Collider2D[] grounds = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRange, goundLayers);
            if (grounds.Length > 0f)
            {
                onGround = true;
                playerAnim.SetBool("isJumping", false);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                cayoteTimeCounter = cayoteTimeCounter + Time.deltaTime;
                if (cayoteTimeCounter > cayoteTime)
                {
                    onGround = false;
                    playerAnim.SetBool("isJumping", true);
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                playerAnim.SetBool("isJumping", false);
            }
        }
        private void JumpAction()
        {
            if (onGround && currentlyJumping == false)
            {
                
                jumpPhase += 1;

                desiredJump = false;

                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
                
                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                else if (velocity.y < 0f)
                {
                    jumpSpeed += Mathf.Abs(body.velocity.y);
                }
                velocity.y += jumpSpeed;
                currentlyJumping = true;
                
                onGround = false;
                ground.cayoteTimeCounter = 0;
            }           
        }
    }
}

