using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : MonoBehaviour
{
    private const float topSpeed = 6;
    private const float acceleration = 0.3f;

    private Rigidbody2D rb;
    private LayerMask groundLayer;
    private Transform groundCheck;
    private Transform wallCheck;
    private GameObject player;
    private Animator anim;

    private float movementSpeed;
    private float horizontalDirection;
    private float newDirection;
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
        if (!isWaiting) {
            ChangeDirection();
        }
        if (Mathf.Abs(movementSpeed) < topSpeed) {
            movementSpeed = rb.velocity.x + acceleration * horizontalDirection;
        } else {
            movementSpeed = (topSpeed - 1) * horizontalDirection;
        }
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void ChangeDirection()
    {
        horizontalDirection = player.transform.position.x > this.transform.position.x ? 1 : -1;
        if (newDirection != horizontalDirection) {
            newDirection = horizontalDirection;
            StartCoroutine(Wait(0.5f));
        }
    }

    private IEnumerator Wait(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.5f);
        isWaiting = false;
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
        if (isFacingRight && horizontalDirection < 0 || !isFacingRight && horizontalDirection > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
