using System;
using System.Diagnostics;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        if (other.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
