using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : MonoBehaviour
{
    private const float topSpeed = 7;
    private const float acceleration = 0.3f;

    private Rigidbody2D rb;
    private LayerMask groundLayer;
    private Transform groundCheck;
    private Transform wallCheck;
    private GameObject player;
    private Animator anim;

    private float movementSpeed;
    private float horizontalDirection;
    private bool isFacingRight;
    private bool isWaiting;
    private bool isRunning;
    private bool movingRight;
    
    private void Start()
    {
        isRunning = true;
        isWaiting = false;
        movementSpeed = 0f;
        isFacingRight = true;
        horizontalDirection = 1;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        wallCheck = transform.Find("WallCheck");
        groundLayer = LayerMask.GetMask("Ground");
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(movementSpeed));
        anim.SetBool("GroundCheck", IsGrounded());
        anim.SetBool("Running", isRunning);
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        ChangeDirection();

        if (movementSpeed >= topSpeed) {
            movementSpeed = topSpeed - acceleration;
        } else if (movementSpeed <= -topSpeed) {
            movementSpeed = -topSpeed + acceleration;
        } else {
            movementSpeed = movementSpeed + acceleration * horizontalDirection;
        }

        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void ChangeDirection()
    {
        horizontalDirection = player.transform.position.x > transform.position.x ? 1 : -1;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool HitWall()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontalDirection < 0 || !isFacingRight && horizontalDirection > 0) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
