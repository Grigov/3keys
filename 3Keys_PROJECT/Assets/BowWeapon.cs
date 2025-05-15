using UnityEngine;

public class BowWeapon : Weapon
{
    [Header("Bow Settings")]
    public GameObject arrowPrefab;
    public float arrowSpeed = 10f;

    protected override void PerformAttack(Vector2 direction)
    {
        if (arrowPrefab == null) return;

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * arrowSpeed;
        }

        // Повернуть стрелу в сторону полёта
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
