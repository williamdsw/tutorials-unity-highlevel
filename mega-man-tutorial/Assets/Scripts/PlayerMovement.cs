using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // FIELDS

    [SerializeField]
    private float horizontalSpeed = 0;

    [SerializeField]
    private bool isHoldingJumpButton = false;


    // OVERRIDED FUNCTIONS

    private void Start () 
    {
        
    }

    private void Update () 
    {

    }

    // INPUT ACTIONS FUNCTIONS

    public void OnMovement (InputAction.CallbackContext callback)
    {
        horizontalSpeed = callback.ReadValue<float>();
    }

    public void OnJump (InputAction.CallbackContext callback) 
    {
        if (callback.started)
        {
            Debug.Log ("Iniciou pulo");
        }

        isHoldingJumpButton = callback.performed;
    }

}
