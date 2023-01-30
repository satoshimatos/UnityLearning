using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform wallCheck;
    protected LayerMask groundLayer;

    protected float horizontal;
    protected int direction;
    protected bool isFacingRight;

    protected void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        rb = gameObject.GetComponent<Rigidbody2D>();
        wallCheck = transform.Find("WallCheck");
        isFacingRight = true;
        direction = 1;
    }

    protected void MoveSideways(int direction, float speed)
    {
        switch (direction) {
            default:
                case -1:
                    horizontal = speed * -1;
                    break;
                case 1:
                    horizontal = speed;
                    break;
        }
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    protected void Flip() {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            wallCheck.localScale = localScale;
        }
    }

    protected bool HitWall() {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }
}
