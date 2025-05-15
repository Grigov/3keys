using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint; // точка, откуда стреляет
    public float fireCooldown = 1.5f;
    public float detectionRange = 8f;
    public float arrowSpeed = 10f;
    public float damage = 10f;

    private float lastFireTime;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > detectionRange) return;

        if (Time.time - lastFireTime >= fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.transform.right = dir;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = dir * arrowSpeed;

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
            arrowScript.damage = damage;
    }
}
