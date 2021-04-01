using Global;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
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
        private PlayerMovement movement;
        private PlayerWeapon weapon;
        private Transform bulletSpawner;
        private Animator animator;

        public Rigidbody2D PrefabBullet { get => prefabBullet; set => prefabBullet = value; }

        private void Awake()
        {
            this.movement = GetComponent<PlayerMovement>();
            this.weapon = GetComponent<PlayerWeapon>();
            this.animator = GetComponent<Animator>();
            this.bulletSpawner = this.transform.Find("BulletSpawner");
        }

        private void Update()
        {
            if (shootPhase.started)
            {
                currentChargingSize += Time.deltaTime * ((totalChargeSize - 1) / totalChargeTimeToIncrement);
            }

            currentChargingSize = Mathf.Clamp(currentChargingSize, 1, totalChargeSize);
        }

        public void OnShoot(InputAction.CallbackContext callback)
        {
            this.shootPhase = callback;
            if (shootPhase.canceled)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (UIManager.Instance.IsPaused) return;
            if (Time.time < currentFireRate || !PrefabBullet) return;
            if (weapon.Current.EnergyAmount > 0 && weapon.Energy < 0) return;

            weapon.Energy -= weapon.Current.EnergyAmount;
            UIManager.Instance.UpdateEnergyBar(weapon.Energy);

            movement.Animator.SetTrigger("shoot");
            currentFireRate = (Time.time + fireRate);

            Vector2 spawnPosition = (bulletSpawner ? bulletSpawner.position : this.transform.position);
            Rigidbody2D newBullet = Instantiate(PrefabBullet, spawnPosition, Quaternion.identity);
            if (weapon.Current.CanCharge)
            {
                newBullet.transform.localScale *= currentChargingSize;
                Damager damager = newBullet.GetComponent<Damager>();
                if (damager)
                {
                    damager.Damage *= currentChargingSize;
                    Debug.Log("Damage: " + damager.Damage);
                }
            }

            newBullet.velocity = (Vector2.right * shootSpeed * movement.CurrentDirection);
            currentChargingSize = 1;
        }
    }
}