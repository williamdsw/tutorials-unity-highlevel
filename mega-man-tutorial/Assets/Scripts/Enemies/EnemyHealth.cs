using Global;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : Damageable
    {
        [SerializeField] private GameObject explosion;

        public override void Death()
        {
            Destroy(this.gameObject);
        }

        public void UpdateBossHealth()
        {
            UIManager.Instance.UpdateBossHealthBar(currentHealth);
        }

        public void Explosion()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}