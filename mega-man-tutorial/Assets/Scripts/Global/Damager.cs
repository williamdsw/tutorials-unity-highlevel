using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool destroyOnDamage;
    [SerializeField] private float timeToDestroy;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            ApplyDamage(damageable);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            ApplyDamage(damageable);
        }
    }

    private void ApplyDamage(Damageable damageable)
    {
        damageable.TakeDamage(damage);
        if (destroyOnDamage)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}
