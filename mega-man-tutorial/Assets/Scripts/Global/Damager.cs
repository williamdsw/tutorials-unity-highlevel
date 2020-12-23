using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnDamage;
    [SerializeField] private float timeToDestroy;

    public float Damage { get => damage; set => damage = value; }

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
        damageable.TakeDamage(Damage);
        if (destroyOnDamage)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}
