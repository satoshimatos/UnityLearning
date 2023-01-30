using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private LayerMask groundLayer;
    private Transform groundCheck;
    private Transform wallCheck;
    private GameObject player;
    private Animator anim;

    private float speed = 5f;
    private float horizontal;
    private float jumpingPower = 15f;
    private float playerXPosition;
    private bool isFacingRight = true;
    private bool isWaiting;
    private bool isRunning = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        isWaiting = false;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck");
    }

    private void Update()
    {
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        anim.SetBool("GroundCheck", IsGrounded());
        anim.SetBool("Running", isRunning);
    }

    private void FixedUpdate()
    {
        Flip();
        Move();
    }

    private void Move()
    {
        if (!isWaiting) {
            StartCoroutine(FinishAnimation(1));
        }
    }

    private IEnumerator FinishAnimation(int time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
        rb.velocity = new Vector2(MoveDirection() * speed, rb.velocity.y);
        Flip();
    }

    private float MoveDirection()
    {
        horizontal = player.transform.position.x > this.transform.position.x ? 1 : -1;
        return horizontal;
    }

    private bool ChangeDirection(int currentDirection)
    {
        float newDirection = MoveDirection();
        if (currentDirection != newDirection) {
            Debug.Log("Change");
            return true;
        }
        Debug.Log("No change");
        return false;
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
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
