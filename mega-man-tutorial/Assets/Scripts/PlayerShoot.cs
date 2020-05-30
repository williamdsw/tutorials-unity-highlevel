using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    // FIELDS

    // Config
    [SerializeField] private Rigidbody2D prefabBullet;
    [SerializeField] private float shootSpeed = 20f;
    [SerializeField] private float fireRate = 0.15f;

    // State
    [SerializeField] private float currentFireRate;

    // Cached
    private InputAction.CallbackContext shootPhase;
    private PlayerMovement playerMovement;

    // OVERRIDED FUNCTIONS

    private void Awake () 
    {
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    private void Start ()
    {
        
    }

    private void Update ()
    {
        
    }

    // INPUT ACTION EVENTS

    public void OnShoot (InputAction.CallbackContext callback)
    {
        this.shootPhase = callback;
        if (shootPhase.canceled)
        {
            Shoot ();
        }
    }

    // HELPER FUNCTIONS

    private void Shoot ()
    {
        if (Time.time < currentFireRate)
        {
            return;
        }

        currentFireRate = (Time.time + fireRate);
        Rigidbody2D newBullet = Instantiate (prefabBullet, this.transform.position, Quaternion.identity);
        newBullet.velocity = (Vector2.right * shootSpeed * playerMovement.CurrentDirection);
    }
}
