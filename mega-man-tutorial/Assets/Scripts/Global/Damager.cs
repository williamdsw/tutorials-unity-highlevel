using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    // FIELDS

    [SerializeField] private int damage;
    [SerializeField] private bool destroyOnDamage;
    [SerializeField] private float timeToDestroy;

    // MONOBEHAVIOUR FUNCTIONS

    private void OnCollisionEnter2D (Collision2D other) 
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            ApplyDamage (damageable);
        }
    }

    private void OnTriggerEnter2D (Collider2D other) 
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            ApplyDamage (damageable);
        }
    }

    // FUNCTIONS

    private void ApplyDamage (Damageable damageable)
    {
        damageable.TakeDamage (damage);
        if (destroyOnDamage)
        {
            Destroy (gameObject, timeToDestroy);
        }
    }
}
