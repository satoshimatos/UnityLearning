using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BipodAI : Enemy
{
    private const float speed = 2;

    private float jumpVelocity;
    private float jumpHeight;
    private bool isOnCooldown = false;
    private bool canJump = true;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AnimationController();
    }

    private void Update()
    {
        if (HitWall()) {
            if (direction == -1) {
                direction = 1;
            } else {
                direction = -1;
            }
        }
    }

    private void Jump()
    {
        isOnCooldown = true;
        if (IsGrounded()) {
            rb.AddForce(new Vector2(speed * direction, 6), ForceMode2D.Impulse);
        }
    }

    private void AnimationController()
    {
        anim.SetBool("IsOnCooldown", isOnCooldown);
        anim.SetBool("IsGrounded", IsGrounded());
    }
}
