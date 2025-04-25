using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public float damage = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 0.5f;
    public LayerMask enemyLayer;

    protected float lastAttackTime;

    public virtual void Attack(Vector2 attackDirection)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;
        PerformAttack(attackDirection);
    }

    protected abstract void PerformAttack(Vector2 direction);
}