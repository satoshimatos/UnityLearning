using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected GameObject player;
    protected Transform wallCheck;
    protected LayerMask groundLayer;
    protected Transform groundCheck;
    
    protected float horizontal;
    
    protected int direction;
    
    protected bool isFacingRight;

    protected void Start()
    {
        direction       = 1;
        isFacingRight   = true;
        player          = GameObject.Find("Player");
        wallCheck       = transform.Find("WallCheck");
        groundLayer     = LayerMask.GetMask("Ground");
        rb              = gameObject.GetComponent<Rigidbody2D>();
        groundCheck     = transform.Find("GroundCheck");
    }

    // Constantly moves the game object left or right depending on the direction provided
    protected void ConstantMoveSideways(int direction, float speed)
    {
        switch (direction) {
            default:
                case -1:
                    horizontal = -speed;
                    break;
                case 1:
                    horizontal = speed;
                    break;
        }
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    // Flips the game object according to an x vector
    protected void Flip(int vectorX)
    {
        if (isFacingRight && vectorX < 0 || !isFacingRight && vectorX > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            wallCheck.localScale = localScale;
        }
    }

    // <requires a transform WallCheck object> Checks whether this game object is touching the ground on the sides
    protected bool HitWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }

    // <requires a transform GroundCheck object> Checks whether this game object is touching the ground
    protected bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Returns the vector pointing to the player x position
    protected int GetXVectorByPlayerXPosition(float playerXPosition)
    {
        return playerXPosition > transform.position.x ? 1 : -1;
    }
}
