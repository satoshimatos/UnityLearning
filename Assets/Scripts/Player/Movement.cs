using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    private float speed = 5f;
    private float horizontal;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    
    private void Update() {
        ManageInputs();
        Jump();
        Flip();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded()) {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }

    private void Move() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void ManageInputs() {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
