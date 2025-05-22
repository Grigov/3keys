using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private bool rotateOnlyInAggro = true;
    [SerializeField] private float angleOffset = 90f;

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
    private bool lootDropped = false;

    [Header("Loot")]
    public GameObject[] lootPrefabs;
    public float dropChance = 1f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAggressive || !rotateOnlyInAggro)
        {
            RotateTowardsPlayer();
        }

        if (player == null || health.IsDead())
        {
            if (health.IsDead() && !lootDropped)
            {
                DropLoot();
                lootDropped = true;
            }
            return;
        }

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

    private void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void AttackPlayer()
    {

        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            player.GetComponent<Health>().TakeDamage(damage);
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

    void DropLoot()
    {
        if (lootPrefabs.Length == 0) return;

        foreach (var loot in lootPrefabs)
        {
            if (UnityEngine.Random.value <= dropChance)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }

}