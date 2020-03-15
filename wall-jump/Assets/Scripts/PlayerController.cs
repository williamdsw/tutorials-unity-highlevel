using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // FIELDS

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float footOffset = 0.4f;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool onGround;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float horizontalJumpForce = 6f;
    [SerializeField] private float horizontal;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private int direction = 1;
    [SerializeField] private bool canMove = true;

    [Header("Wall")]
    [SerializeField] private bool onWall = false;
    [SerializeField] private Vector3 wallOffset;
    [SerializeField] private float wallRadius;
    [SerializeField] private float maxFallSpeed = -1f;
    [SerializeField] private float wallJumpDuration = 0.25f;
    [SerializeField] private bool jumpFromWall;
    [SerializeField] private float jumpFinish;
    [SerializeField] private LayerMask wallLayer;

    private bool clearInputs;

    private Rigidbody2D rigidBody2D;
    private Animator animator;

    // MONOBEHAVIOUR FUNCTIONS

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInputs();
        PhysicsCheck();
    }

    private void FixedUpdate()
    {
        GroundMovement();
        AirMovement();
        clearInputs = true;
    }

    private void OnDrawGizmos()
    {
        // Desenha esferas de colisoes
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position + wallOffset, wallRadius);
        Gizmos.DrawWireSphere(this.transform.position - wallOffset, wallRadius);
    }

    // HELPER FUNCTIONS

    private void GroundMovement()
    {
        if (!canMove) return;

        float x = horizontal * speed;
        rigidBody2D.velocity = new Vector2(x, rigidBody2D.velocity.y);

        if (x * direction < 0f)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void AirMovement()
    {
        if (jumpPressed && onGround)
        {
            jumpPressed = false;
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        else if (jumpPressed && onWall && !onGround)
        {
            canMove = false;
            jumpFinish = Time.time + wallJumpDuration;
            jumpPressed = false;
            jumpFromWall = true;
            Flip();

            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.AddForce(new Vector2(horizontalJumpForce * direction, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckInputs()
    {
        if (clearInputs)
        {
            jumpPressed = false;
            clearInputs = false;
        }

        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");

        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
        }

        if (jumpFromWall)
        {
            if (Time.time > jumpFinish)
            {
                jumpFromWall = false;
            }
        }

        if (!jumpFromWall && !canMove)
        {
            if (Input.GetAxis("Horizontal") != 0f || onGround)
            {
                canMove = true;
            }
        }


    }

    private void PhysicsCheck()
    {
        onGround = false;
        onWall = false;

        RaycastHit2D leftFoot = Raycast(groundCheck.position + new Vector3(-footOffset, 0), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFoot = Raycast(groundCheck.position + new Vector3(footOffset, 0), Vector2.down, groundDistance, groundLayer);

        if (leftFoot || rightFoot)
        {
            onGround = true;
        }

        // verificacao colisao com parades
        bool rightWall = Physics2D.OverlapCircle(this.transform.position + wallOffset, wallRadius, wallLayer);
        bool leftWall = Physics2D.OverlapCircle(this.transform.position - wallOffset, wallRadius, wallLayer);
        onWall = (rightWall || leftWall);

        if (onWall)
        {
            if (rigidBody2D.velocity.y < maxFallSpeed)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, maxFallSpeed);
            }
        }

        animator.SetBool("OnGround", onGround);
        animator.SetBool("OnWall", onWall);

    }

    public RaycastHit2D Raycast(Vector2 origin, Vector2 rayDirection, float length, LayerMask mask, bool drawRay = true)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, length, mask);

        if (drawRay)
        {
            Color color = hit ? Color.red : Color.green;
            Debug.DrawRay(origin, rayDirection * length, color);
        }

        return hit;
    }
}