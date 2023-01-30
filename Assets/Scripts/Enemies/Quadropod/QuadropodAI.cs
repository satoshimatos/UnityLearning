using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadropodAI : MonoBehaviour
{
    const int speed = 2;

    private Rigidbody2D rb;
    private Transform wallCheck;
    
    private int horizontal;
    
    private string direction = "right";
    
    private bool isFacingRight = true;
    
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wallCheck = transform.Find("WallCheck");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Flip();
        if (HitWall()) {
            if (direction == "left") {
                direction = "right";
            } else {
                direction = "left";
            }
        }
        Move(direction);
    }

    private void Move(string direction)
    {
        switch (direction) {
            default:
                case "left":
                    horizontal = speed * -1;
                    break;
                case "right":
                    horizontal = speed;
                    break;
        }
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            wallCheck.localScale = localScale;
        }
    }

    private bool HitWall() {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }
}
