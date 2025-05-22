using System.Diagnostics;
using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip damageSound;
    public float damageVolume = 1f;

    private AudioSource audioSource;

    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;

    public bool IsDead() => currentHealth <= 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = (this.CompareTag("Player")) ? DataPlayer.health : maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, PlayerHealth.Instance.maxHealth);
        DataPlayer.health = (int)currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible || IsDead()) return;

        currentHealth -= damage;

        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound, damageVolume);
        }

        if (this.CompareTag("Player"))
        {
            DataPlayer.health = (int)currentHealth;
        }

        if (IsDead())
        {
            Die();
        }
    }

    public void Die()
    {
        if (IsDead())
        {
            if (this.CompareTag("Player"))
            {
                DestroyAllDontDestroyObjects();
                GetComponent<SceneObject>()?.MarkAsDestroyed();
                SceneManager.LoadScene(2);
                Time.timeScale = 0f;
            }
            else if (this.CompareTag("Enemy"))
            {
                GetComponent<SceneObject>()?.MarkAsDestroyed();
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
    private void DestroyAllDontDestroyObjects()
    {
        GameObject temp = new GameObject("TempSceneLoader");
        DontDestroyOnLoad(temp);

        UnityEngine.SceneManagement.Scene dontDestroyScene = temp.scene;
        Destroy(temp);

        GameObject[] rootObjects = dontDestroyScene.GetRootGameObjects();

        foreach (GameObject go in rootObjects)
        {
            Destroy(go);
        }
    }
}