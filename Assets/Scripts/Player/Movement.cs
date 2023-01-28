using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    
    private float speed = 5f;
    private float horizontal;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    private bool takingDamage;

    private void Awake() {
        takingDamage = false;
    }

    private void Update() {
        Debug.Log(Mathf.Abs(rb.velocity.y));
        Animate();
        ManageInputs();
        Jump();
        Flip();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Animate() {
        anim.SetBool("TakingDamage", takingDamage);
        anim.SetBool("GroundCheck", isGrounded());
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded()) {
            anim.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }

        if (rb.velocity.y == 0) {
            anim.SetBool("IsJumping", false);
        }
    }  

    private void Move() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void ManageInputs() {
        horizontal = Input.GetAxis("Horizontal");
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
