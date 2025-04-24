using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    private float moveInputY;
    public float moveSpeed = 5f;
    public float run = 1.5f;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? moveSpeed * run : moveSpeed;

        rb.velocity = new Vector2(horizontalInput * currentSpeed, verticalInput * currentSpeed);

        if (horizontalInput != 0)
        {
            sprite.flipX = horizontalInput < 0;
        }
        if (verticalInput != 0)
        {
            sprite.flipY = verticalInput < 0;
        }
    }
}
