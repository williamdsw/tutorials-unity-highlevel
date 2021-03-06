﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Global Configuration")]
        [SerializeField] private float movementSpeed = 5f;
        private float leftFootOffset = -0.35f;
        private float rightFootOffset = 0.22f;
        private float groundOffset = 0.85f;
        private float groundDistance = 0.15f;
        [SerializeField] private LayerMask groundLayerMask;

        [Header("Ladder")]
        [SerializeField] private float climbSpeed = 3f;
        [SerializeField] private LayerMask ladderMask;

        [SerializeField] private bool isClimbing = false;
        [SerializeField] private float checkRadius = 0.3f;
        [SerializeField] private Transform ladder;

        [Header("Jump")]
        [SerializeField] private bool isHoldingJumpButton = false;
        [SerializeField] private bool hasJumped = false;
        [SerializeField] private bool isJumping = false;
        [SerializeField] private float currentJumpTime;
        [SerializeField] private bool isOnGround = false;
        [SerializeField] private float jumpForce = 3f;
        [SerializeField] private float jumpHoldForce = 1;
        private float jumpHoldDuration = 0.15f;
        [SerializeField] private float currentCoyoteTime;
        [SerializeField] private float coyoteTimeDuration = 0.1f;

        [Header("States")]
        [SerializeField] private float currentHorizontalInput = 0;
        [SerializeField] private int currentDirection = 1;
        [SerializeField] private float currentVerticalInput = 0;
        [SerializeField] private bool canMove = true;

        private Rigidbody2D rigidBody;
        private Animator animator;
        private Collider2D collider2d;

        public int CurrentDirection { get => currentDirection; }
        public Animator Animator { get => animator; }

        private void Awake()
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
            this.animator = this.GetComponent<Animator>();
            this.collider2d = this.GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            CheckPhysics();
            GroundMovement();
            AirMovement();
            ClimbLadder();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            {
                ladder = other.transform;
            }
        }

        public void OnMovement(InputAction.CallbackContext callback)
        {
            currentHorizontalInput = callback.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                hasJumped = true;
            }

            isHoldingJumpButton = callback.performed;
        }

        public void OnClimb(InputAction.CallbackContext callback)
        {
            currentVerticalInput = callback.ReadValue<float>();
        }

        // HELPER FUNCTIONS

        private void CheckPhysics()
        {
            if (!canMove) return;

            isOnGround = false;
            RaycastHit2D leftFoot = this.CheckCollision(new Vector2(leftFootOffset, -groundOffset), Vector2.down, groundDistance, groundLayerMask);
            RaycastHit2D rightFoot = this.CheckCollision(new Vector2(rightFootOffset, -groundOffset), Vector2.down, groundDistance, groundLayerMask);

            if (leftFoot || rightFoot)
            {
                isOnGround = true;
            }

            animator.SetBool("onGround", isOnGround);
        }

        private void GroundMovement()
        {
            if (!canMove) return;

            float xVelocity = (movementSpeed * currentHorizontalInput);
            this.rigidBody.velocity = new Vector2(xVelocity, this.rigidBody.velocity.y);

            if (currentDirection * xVelocity < 0)
            {
                Flip();
            }

            if (isOnGround)
            {
                currentCoyoteTime = Time.time + coyoteTimeDuration;
            }

            animator.SetFloat("speed", Mathf.Abs(currentHorizontalInput));
        }

        private void AirMovement()
        {
            if (hasJumped && (isOnGround || currentCoyoteTime > Time.time))
            {
                hasJumped = false;
                isJumping = true;

                rigidBody.velocity = Vector2.zero;
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                currentJumpTime = (Time.time + jumpHoldDuration);
                currentCoyoteTime = Time.time;
            }

            if (isJumping)
            {
                if (isHoldingJumpButton)
                {
                    rigidBody.AddForce(Vector2.up * jumpHoldForce, ForceMode2D.Impulse);
                }

                if (currentJumpTime <= Time.time)
                {
                    isJumping = false;
                }
            }

            hasJumped = false;
        }

        private RaycastHit2D CheckCollision(Vector2 offset, Vector2 direction, float distance, LayerMask layerMask)
        {
            Vector2 position = this.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(position + offset, direction, distance, layerMask);
            Color color = (hit ? Color.red : Color.green);
            Debug.DrawRay(position + offset, direction * distance, color);
            return hit;
        }

        private void Flip()
        {
            currentDirection *= -1;
            Vector3 currentScale = this.transform.localScale;
            currentScale.x *= -1;
            this.transform.localScale = currentScale;
        }

        private void ClimbLadder()
        {
            bool isGoingUp = Physics2D.OverlapCircle(this.transform.position, checkRadius, ladderMask);
            bool isGoindDown = Physics2D.OverlapCircle(this.transform.position + Vector3.down, checkRadius, ladderMask);

            if (currentVerticalInput != 0 && IsTouchingLadder())
            {
                isClimbing = true;
                rigidBody.isKinematic = true;
                canMove = false;

                float xPosition = ladder.position.x;
                this.transform.position = new Vector2(xPosition, this.transform.position.y);
            }

            if (isClimbing)
            {
                if (!isGoingUp && currentVerticalInput >= 0)
                {
                    FinishClimb();
                    return;
                }

                if (!isGoindDown && currentVerticalInput <= 0)
                {
                    FinishClimb();
                    return;
                }

                float y = (currentVerticalInput * climbSpeed);
                rigidBody.velocity = new Vector2(0, y);
            }


        }

        private void FinishClimb()
        {
            isClimbing = false;
            rigidBody.isKinematic = false;
            canMove = true;
        }

        private bool IsTouchingLadder()
        {
            return collider2d.IsTouchingLayers(ladderMask);
        }
    }
}