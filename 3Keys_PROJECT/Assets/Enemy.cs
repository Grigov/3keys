using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime;
    private Health health;
    private Rigidbody2D rb;
    public float aggressionRange = 8f;
    private bool isAggressive;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null || health.IsDead()) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            Patrule();
        }
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (!isAggressive && distToPlayer <= aggressionRange)
        {
            isAggressive = true;
        }
    }
    void Patrule()
    {
        rb.velocity = Vector2.zero;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Простой поворот спрайта
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;

        // Проверяем, что игрок все еще в радиусе атаки
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            player.GetComponent<Health>().TakeDamage(damage);
            Debug.Log($"Враг нанес {damage} урона игроку!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}