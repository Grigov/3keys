using UnityEngine;
using System.Collections;

public class SwordWeapon : Weapon
{
    [Header("Rotation Animation")]
    [SerializeField] private AnimationCurve rotationCurve;

    [Header("Attack Settings")]
    [SerializeField] private float attackAngle = 90f;


    private bool isSwinging;


    protected override void PerformAttack(Vector2 direction)
    {
        if (!isSwinging)
        {
            StartCoroutine(SwingAnimation(direction));
        }
        //if (slashEffect)
        //{
        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    Quaternion rotation = Quaternion.Euler(0, 0, angle);

        //    Instantiate(slashEffect, transform.position, rotation);
        //}
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange,
            enemyLayer
        );

        foreach (var hit in hits)
        {
            if (hit == null) continue;

            if (!hit.CompareTag("Enemy")) continue;

            Health enemyHealth = hit.GetComponent<Health>();
            if (enemyHealth == null || enemyHealth.IsDead()) continue;

            Vector2 toEnemy = (hit.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(direction, toEnemy);

            if (angle <= attackAngle / 2)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Hit: {hit.name} | Damage: {damage} | Angle: {angle}°");
            }
        }
    }

    private IEnumerator SwingAnimation(Vector2 direction)
    {
        float windUpDuration = 0.1f;     // замах
        float swingDuration = 0.15f;     // удар
        float swingAngle = -90f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float windUpAngle = angle - swingAngle / 2;
        float attackAngle = angle + swingAngle / 2;

        float elapsed = 0f;
        while (elapsed < windUpDuration)
        {
            float t = elapsed / windUpDuration;
            float currentAngle = Mathf.Lerp(angle, windUpAngle, t);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < swingDuration)
        {
            float t = elapsed / swingDuration;
            float currentAngle = Mathf.Lerp(windUpAngle, attackAngle, t);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}