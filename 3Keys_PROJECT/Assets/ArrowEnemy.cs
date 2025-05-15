using System;
using System.Diagnostics;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        if (other.CompareTag("Enemy"))
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
