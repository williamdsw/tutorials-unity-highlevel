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
    [SerializeField] private float totalChargeSize = 3;
    [SerializeField] private float totalChargeTimeToIncrement = 2;

    // State
    [SerializeField] private float currentFireRate;
    [SerializeField] private float currentChargingSize = 1;

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
        if (shootPhase.started)
        {
            currentChargingSize += Time.deltaTime * ((totalChargeSize - 1) / totalChargeTimeToIncrement);
        }

        currentChargingSize = Mathf.Clamp (currentChargingSize, 1, totalChargeSize);
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
        newBullet.transform.localScale *= currentChargingSize;
        currentChargingSize = 1;

        Destroy (newBullet.gameObject, 2f);
    }
}
