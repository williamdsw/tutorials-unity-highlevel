using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalMan : MonoBehaviour
{
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootSpawn;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float jumpInterval = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform player;
    [SerializeField] private RollingPlatform rollingPlatform;

    private float nextJump;
    private float nextFire;
    private bool isOnGround;

    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("SwitchPlatformDirection", 10f, 10f);
    }

    private void FixedUpdate()
    {
        AimingPlayer();

        if (Time.time > nextJump)
        {
            Jump();
        }

        isOnGround = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2, groundLayer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(transform.position, Vector2.down * 1, color);
        isOnGround = hit;

        if (!isOnGround && Time.time > nextFire)
        {
            Shoot();
        }
    }

    public void StartBossFight() { }

    private void AimingPlayer()
    {
        Vector2 direction = (player.position - shootSpawn.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        shootSpawn.rotation = rotation;
    }

    private void SwitchPlatformDirection()
    {
        rollingPlatform.CurrentDirection *= 1;
    }

    private void Jump()
    {
        nextJump = (Time.time + jumpInterval);
        float force = Random.Range(jumpForce.x, jumpForce.y);
        rigidBody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        nextFire = (Time.time + fireRate);
        Instantiate(bullet, shootSpawn.position, shootSpawn.rotation);
    }


}
