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
        [SerializeField, Range(0f, 100f)] private float dashingPower = 20f;
        [SerializeField, Range(0f, 1f)] private float dashingTime = 0.2f;
        [SerializeField, Range(0f, 10f)] private float dashingCooldown = 1f;

        private Controller controller;
        private Vector2 direction;
        private Vector2 desiredVelocity;
        private Vector2 velocity;
        private Rigidbody2D body;
        private Ground ground;
        private GameObject DashItem;

        private float maxSpeedChange;
        private float acceleration;
        private float InputHorizontal;

        private bool dashUnlocked = false;
        private bool onGround;
        private bool canDash = true;
        public bool isDashing;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();
            DashItem = GameObject.Find("DashItem");
        }

        private void Update()
        {
            direction.x = controller.input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.Friction, 0f);
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
                gameObject.transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
            }
            if (InputHorizontal < 0)
            {
                gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0f);
            }
        }
        private IEnumerator Dash()
        {
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
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
