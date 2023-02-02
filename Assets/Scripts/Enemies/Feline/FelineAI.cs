using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : Enemy
{
    private const float topSpeed = 7;
    private const float acceleration = 0.3f;

    private Animator anim;

    private float movementSpeed;
    private bool isWaiting;
    private bool isRunning;
    
    private void Awake()
    {
        isRunning = true;
        isWaiting = false;
        movementSpeed = 0f;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(movementSpeed));
        anim.SetBool("GroundCheck", IsGrounded());
        anim.SetBool("Running", isRunning);
        Flip(GetXVectorByPlayerXPosition(player.transform.position.x));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (movementSpeed >= topSpeed) {
            movementSpeed = topSpeed - acceleration;
        } else if (movementSpeed <= -topSpeed) {
            movementSpeed = -topSpeed + acceleration;
        } else {
            movementSpeed = movementSpeed + acceleration * GetXVectorByPlayerXPosition(player.transform.position.x);
        }

        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }
}
