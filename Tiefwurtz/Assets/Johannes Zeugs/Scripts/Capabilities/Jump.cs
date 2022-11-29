using UnityEngine;

namespace Tiefwurtz
{
    [RequireComponent(typeof(Controller))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
        [SerializeField, Range(0f, 10f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
        [SerializeField, Range(0f, 20f)] private float maxFallSpeed = 10f;
        [SerializeField, Range(0f, 1f)] private float jumpTimeAfterFalling = 0.2f;

        private Controller controller;
        private Rigidbody2D body;
        private Ground ground;
        private Vector2 velocity;

        private float jumpTimeAfterFallingCounter;
        private int jumpPhase;
        private float defaultGravityScale;
        private float jumpSpeed;
        private bool currentlyJumping;

        private bool desiredJump;
        private bool onGround;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();

            defaultGravityScale = 1f;
        }

        void Update()
        {
            desiredJump |= controller.input.RetrieveJumpInput();


            // Jump after Falling
            if (!currentlyJumping && !onGround)
            {
                jumpTimeAfterFallingCounter += Time.deltaTime;
            }
            else
            {
                jumpTimeAfterFallingCounter = 0;
            }
        }

        private void FixedUpdate()
        {
            onGround = ground.OnGround;
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
        private void JumpAction()
        {
            if (onGround || jumpPhase < maxAirJumps || (jumpTimeAfterFallingCounter > 0.03f) && (jumpTimeAfterFallingCounter < jumpTimeAfterFalling))
            {
                onGround = false;
                jumpPhase += 1;

                desiredJump = false;
                jumpTimeAfterFallingCounter = 0;

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
            }           
        }
    }
}

