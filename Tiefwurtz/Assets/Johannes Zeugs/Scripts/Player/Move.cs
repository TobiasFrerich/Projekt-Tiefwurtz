using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Tiefwurtz
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trail;

        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
        [SerializeField, Range(0f, 300f)] private float dashingPower = 20f;
        [SerializeField, Range(0f, 1f)] private float dashingTime = 0.2f;
        [SerializeField, Range(0f, 10f)] private float dashingCooldown = 1f;


        [SerializeField] private AudioSource DashSound;
        [SerializeField] private AudioSource Run;

        [SerializeField] private ParticleSystem DashParticals;
        

        [SerializeField] float dashUseDmg = 2f;

        private Controller controller;
        private Vector2 direction;
        private Vector2 desiredVelocity;
        private Vector2 velocity;
        private Rigidbody2D body;
        private Ground ground;
        private GameObject DashItem;
        private PlayerLight playerLight;
        private Animator playerAnim;

        private float maxSpeedChange;
        private float acceleration;
        private float InputHorizontal;

        public static bool dashUnlocked = false;
        private bool onGround;
        private  bool canDash = true;
        public bool isDashing;
        private bool currentlyJumping;

        private void Awake()
        {
            currentlyJumping = GetComponent<Jump>().currentlyJumping;
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();
            DashItem = GameObject.Find("DashItem");
            playerAnim = GetComponent<Animator>();
        }

        private void Update()
        {
            direction.x = controller.input.RetrieveMoveInput();

            if (body.velocity.x != 0f && !playerAnim.GetBool("isJumping"))
            {
                playerAnim.SetBool("isRunning", true);
                Run.enabled = true;
            }
            else
            {
                Run.enabled = false;
                playerAnim.SetBool("isRunning", false);
            }

            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed, 0f);
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashUnlocked)
            {
                StartCoroutine(Dash());
            }
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                return;
            }
            ChecktoFlipSprite();
            onGround = ground.OnGround;
            velocity = body.velocity;

            acceleration = onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            body.velocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == DashItem)
            {
                dashUnlocked = true;
            }
        }
        private void ChecktoFlipSprite()
        {
            InputHorizontal = Input.GetAxisRaw("Horizontal");
            if (InputHorizontal > 0)
            {
                gameObject.transform.localScale = new Vector3(-0.07f, 0.07f, 0f);
            }
            if (InputHorizontal < 0)
            {
                gameObject.transform.localScale = new Vector3(0.07f, 0.07f, 0f);
            }
        }
        private IEnumerator Dash()
        {
            DashParticals.Play();
            ParticleSystem.EmissionModule em = DashParticals.emission;
            em.enabled = true;
            playerAnim.SetBool("isDashing", true);
            DashSound.Play();
            playerLight = GetComponent<PlayerLight>();
            playerLight.backLight.intensity = playerLight.backLight.intensity - dashUseDmg;

            canDash = false;
            isDashing = true;
            float originalGravity = body.gravityScale;
            body.gravityScale = 0f;
            body.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
            trail.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            trail.emitting = false;
            body.gravityScale = originalGravity;
            isDashing = false;
            playerAnim.SetBool("isDashing", false);
            DashParticals.Stop();
            ParticleSystem.EmissionModule emt = DashParticals.emission;
            emt.enabled = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
