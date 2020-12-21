using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D prefabBullet;
    [SerializeField] private float shootSpeed = 20f;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float totalChargeSize = 3;
    [SerializeField] private float totalChargeTimeToIncrement = 2;

    [SerializeField] private float currentFireRate;
    [SerializeField] private float currentChargingSize = 1;

    private InputAction.CallbackContext shootPhase;
    private PlayerMovement playerMovement;
    private Transform bulletSpawner;

    private void Awake () 
    {
        this.playerMovement = this.GetComponent<PlayerMovement>();
        this.bulletSpawner = this.transform.Find ("BulletSpawner");
    }

    private void Update ()
    {
        if (shootPhase.started)
        {
            currentChargingSize += Time.deltaTime * ((totalChargeSize - 1) / totalChargeTimeToIncrement);
        }

        currentChargingSize = Mathf.Clamp (currentChargingSize, 1, totalChargeSize);
    }

    public void OnShoot (InputAction.CallbackContext callback)
    {
        this.shootPhase = callback;
        if (shootPhase.canceled)
        {
            Shoot ();
        }
    }

    private void Shoot ()
    {
        if (Time.time < currentFireRate) return;

        Vector2 spawnPosition = (bulletSpawner ? bulletSpawner.position : this.transform.position);
        currentFireRate = (Time.time + fireRate);
        Rigidbody2D newBullet = Instantiate (prefabBullet, spawnPosition, Quaternion.identity);
        newBullet.velocity = (Vector2.right * shootSpeed * playerMovement.CurrentDirection);
        newBullet.transform.localScale *= currentChargingSize;
        currentChargingSize = 1;

        Destroy (newBullet.gameObject, 2f);
        playerMovement.Animator.SetTrigger ("shoot");
    }
}
