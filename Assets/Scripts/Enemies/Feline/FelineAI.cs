using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    
    private float speed = 5f;
    private float horizontal;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    
    private void Update() {
        Flip();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded() {
        Debug.Log("Floor");
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool hitWall() {
        Debug.Log("Wall");
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
