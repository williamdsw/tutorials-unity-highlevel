using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // FIELDS

    // Config
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float jumpHoldForce = 1;
    private float jumpHoldDuration = 0.15f;

    private float leftFootOffset = - 0.35f;
    private float rightFootOffset = 0.22f;
    private float groundOffset = 0.85f;
    private float groundDistance = 0.15f;
    [SerializeField] private LayerMask groundLayerMask;

    // State
    [SerializeField] private float currentHorizontalInput = 0;
    [SerializeField] private bool isHoldingJumpButton = false;
    [SerializeField] private bool hasJumped = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float currentJumpTime;
    [SerializeField] private bool isOnGround = false;

    // Cached
    private Rigidbody2D myRigidbody;

    // OVERRIDED FUNCTIONS

    private void Awake () 
    {
        this.myRigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Start () 
    {
        
    }

    private void Update () 
    {

    }

    private void FixedUpdate () 
    {
        GroundMovement ();
        AirMovement ();
        CheckPhysics ();
    }

    // INPUT ACTIONS FUNCTIONS

    public void OnMovement (InputAction.CallbackContext callback)
    {
        currentHorizontalInput = callback.ReadValue<float>();
    }

    public void OnJump (InputAction.CallbackContext callback) 
    {
        if (callback.started)
        {
            hasJumped = true;
        }

        isHoldingJumpButton = callback.performed;
    }

    // HELPER FUNCTIONS

    private void CheckPhysics ()
    {
        isOnGround = false;
        RaycastHit2D leftFoot = this.CheckCollision (new Vector2 (leftFootOffset, - groundOffset), Vector2.down, groundDistance, groundLayerMask);
        RaycastHit2D rightFoot = this.CheckCollision (new Vector2 (rightFootOffset, - groundOffset), Vector2.down, groundDistance, groundLayerMask);

        if (leftFoot || rightFoot)
        {
            isOnGround = true;
        }
    }

    private void GroundMovement ()
    {
        float xVelocity = (movementSpeed * currentHorizontalInput);
        this.myRigidbody.velocity = new Vector2 (xVelocity, this.myRigidbody.velocity.y);
    }

    private void AirMovement ()
    {
        if (hasJumped && isOnGround)
        {
            hasJumped = false;
            isJumping = true;

            myRigidbody.velocity = Vector2.zero;
            myRigidbody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);

            currentJumpTime = (Time.time + jumpHoldDuration);
        }

        if (isJumping)
        {
            if (isHoldingJumpButton)
            {
                myRigidbody.AddForce (Vector2.up * jumpHoldForce, ForceMode2D.Impulse);
            }

            if (currentJumpTime <= Time.time)
            {
                isJumping = false;
            }
        }
    }

    private RaycastHit2D CheckCollision (Vector2 offset, Vector2 direction, float distance, LayerMask layerMask)
    {
        Vector2 position = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast (position + offset, direction, distance, layerMask);
        Color color = (hit ? Color.red : Color.green);
        Debug.DrawRay (position + offset, direction * distance, color);
        return hit;
    }

}
