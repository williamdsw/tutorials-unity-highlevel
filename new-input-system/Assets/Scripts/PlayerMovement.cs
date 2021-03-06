﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float horizontal;
        [SerializeField] private bool isJumping;

        private Rigidbody2D rigidBody2D;
        private PlayerInputActions input;

        private void Awake()
        {
            rigidBody2D = this.GetComponent<Rigidbody2D>();
            input = new PlayerInputActions();
        }

        private void Update()
        {
            // isJumping = isJumping || input.PlayerActions.Jump.triggered;
        }

        private void FixedUpdate()
        {
            // horizontal = input.PlayerActions.Movement.ReadValue<float>();
            rigidBody2D.velocity = new Vector2(horizontal * speed, rigidBody2D.velocity.y);

            if (isJumping)
            {
                isJumping = false;
                rigidBody2D.velocity = Vector2.zero;
                rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        // Necessario para habilitar / desabilitar o sistema de input

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        public void Jump(InputAction.CallbackContext callback)
        {
            isJumping = callback.performed;
        }

        public void Movement(InputAction.CallbackContext callback)
        {
            horizontal = callback.ReadValue<float>();
        }
    }
}