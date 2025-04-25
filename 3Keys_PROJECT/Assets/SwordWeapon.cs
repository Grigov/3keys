using UnityEngine;

public class SwordWeapon : Weapon
{
    [Header("Sword Settings")]
    public GameObject slashEffect;
    public float attackAngle = 120f;

    protected override void PerformAttack(Vector2 direction)
    {
        // 1. Визуальный эффект
        if (slashEffect)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Instantiate(slashEffect, transform.position, rotation);
        }

        // 2. Получаем всех врагов в радиусе
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange,
            enemyLayer
        );

        // 3. Фильтрация и нанесение урона
        foreach (var hit in hits)
        {
            if (hit == null) continue;

            // Проверка тега и компонента
            if (!hit.CompareTag("Enemy")) continue;

            Health enemyHealth = hit.GetComponent<Health>();
            if (enemyHealth == null || enemyHealth.IsDead()) continue;

            // Проверка угла атаки
            Vector2 toEnemy = (hit.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(direction, toEnemy);

            if (angle <= attackAngle / 2)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Hit: {hit.name} | Damage: {damage} | Angle: {angle}°");
            }
        }

        // 4. Визуализация (только в редакторе)
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, 1f);
        DrawAttackArc(direction);
    }

    void DrawAttackArc(Vector2 direction)
    {
#if UNITY_EDITOR
        Vector3 startDir = Quaternion.Euler(0, 0, -attackAngle / 2) * direction;
        UnityEditor.Handles.color = new Color(1, 0, 0, 0.1f);
        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.forward,
            startDir,
            attackAngle,
            attackRange
        );
#endif
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}