﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float angleOffset = 90f;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canMove = true;

    [Header("Stamina Settings")]
    [SerializeField] private float staminaDrainRate = 5f;
    [SerializeField] private float staminaRegenRate = 15f;
    [SerializeField] private float regenDelay = 0f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashDistance = 2.5f;

    [Header("Combat Settings")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform weaponHolder;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isMoving;
    private bool isDashing;
    private bool isRunning;
    private float lastMoveTime;
    private float lastDashTime = -10f;
    private Health playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (Input.GetKeyDown(Keybindings.keys["Attack"]) && currentWeapon != null)
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            currentWeapon.Attack(dir);
        }

        if (Input.GetKeyDown(Keybindings.keys["Dash"]) && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
        }

        if (!canMove || playerHealth.IsDead())
        {
            rb.velocity = Vector2.zero;
            return;
        }
        HandleMouseAiming();
        HandleDashInput();

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            currentWeapon.Attack(dir);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Inventory.Instance.slots.Length > 0 && Inventory.Instance.slots[0].item != null)
            {
                var item = Inventory.Instance.slots[0].item;
                item.Use();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        HandleStamina(input);
        HandleMovement(input);
    }

    private void HandleMouseAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.LerpAngle(
            transform.eulerAngles.z,
            targetAngle - angleOffset,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        bool shouldFlip = direction.x < 0;
        if (sprite.flipY != shouldFlip)
        {
            sprite.flipY = shouldFlip;
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(Keybindings.keys["Dash"]) && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        if (DataPlayer.stamina >= 20) 
        {
            DataPlayer.stamina -= 20f;
            isDashing = true;
            lastDashTime = Time.time;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDirection = (mousePos - transform.position).normalized;

            float actualDistance = Mathf.Min(
                dashDistance,
                dashSpeed * dashDuration
            );

            Vector2 targetPos = rb.position + dashDirection * actualDistance;

            float elapsed = 0f;
            while (elapsed < dashDuration)
            {
                rb.MovePosition(Vector2.Lerp(rb.position, targetPos, elapsed / dashDuration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            isDashing = false;
        }
    }

    private void HandleStamina(Vector2 input)
    {
        isRunning = Input.GetKey(Keybindings.keys["Run"]) && input != Vector2.zero;
        isMoving = input != Vector2.zero && !isRunning;

        if (isRunning)
        {
            lastMoveTime = Time.time;
            float drainAmount = staminaDrainRate * Time.fixedDeltaTime;
            DataPlayer.stamina -= Mathf.Round(drainAmount * 10) / 10;
            DataPlayer.stamina = Mathf.Max(DataPlayer.stamina, 0);
        }
        else if (Time.time - lastMoveTime > regenDelay && DataPlayer.stamina < DataPlayer.baseS)
        {
            DataPlayer.stamina += staminaRegenRate * Time.fixedDeltaTime;
            DataPlayer.stamina = Mathf.Min(DataPlayer.stamina, DataPlayer.baseS);
        }
    }

    private void HandleMovement(Vector2 input)
    {
        if (canMove)
        {
            float speedMultiplier = 1f;

            if (DataPlayer.stamina <= 0)
            {
                speedMultiplier = 0.5f;
            }
            else if (isRunning)
            {
                speedMultiplier = runMultiplier;
            }

            Vector2 movement = input.normalized * moveSpeed * speedMultiplier;
            rb.velocity = movement;
        } 
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(newWeapon, weaponHolder);
        currentWeapon.transform.localPosition = Vector3.zero;
    }
    
}