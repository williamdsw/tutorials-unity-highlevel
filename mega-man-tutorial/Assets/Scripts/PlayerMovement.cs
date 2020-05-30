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

    // State
    [SerializeField] private float currentHorizontalInput = 0;
    [SerializeField] private bool isHoldingJumpButton = false;
    [SerializeField] private bool hasJumped = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float currentJumpTime;

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

    private void GroundMovement ()
    {
        float xVelocity = (movementSpeed * currentHorizontalInput);
        this.myRigidbody.velocity = new Vector2 (xVelocity, this.myRigidbody.velocity.y);
    }

    private void AirMovement ()
    {
        if (hasJumped)
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

}
