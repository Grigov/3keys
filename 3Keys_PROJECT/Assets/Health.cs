using System.Diagnostics;
using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isInvincible = false;

    public bool IsDead() => currentHealth <= 0;

    void Start()
    {
        currentHealth = (this.CompareTag("Player")) ? DataPlayer.health : maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        DataPlayer.health = (int)currentHealth;
        UnityEngine.Debug.Log($"Heal! count = {amount}");
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible || IsDead()) return;

        currentHealth -= damage;

        if (this.CompareTag("Player"))
        {
            DataPlayer.health = (int)currentHealth;
        }
        else
        {
        }

        if (IsDead())
        {
            UnityEngine.Debug.Log($"умирает!");
            Die();
        }
    }

    public void Die()
    {
        if (IsDead())
        {
            if (this.CompareTag("Player"))
            {
                SceneManager.LoadScene(2);
                Time.timeScale = 0f;
            }
            else if (this.CompareTag("Enemy"))
            {
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }

}