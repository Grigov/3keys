using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isInvincible = false;

    public bool IsDead() => currentHealth <= 0;

    void Start()
    {
        // Используем сохраненное здоровье или максимальное
        currentHealth = (this.CompareTag("Player")) ? DataPlayer.health : maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible || IsDead()) return;

        currentHealth -= damage;

        if (this.CompareTag("Player"))
        {
            DataPlayer.health = (int)currentHealth;
            Debug.Log($"Игрок получил {damage} урона. Осталось {currentHealth} HP");
        }
        else
        {
            Debug.Log($"{name} получил {damage} урона. Осталось {currentHealth} HP");
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
                Debug.Log("Игрок умер! Game Over");
                // Здесь можно добавить перезагрузку сцены или экран смерти
                Time.timeScale = 0f; // Останавливаем игру
            }
            else if (this.CompareTag("Enemy"))
            {
                Debug.Log($"{name} умер!");
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}