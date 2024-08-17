using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelineAI : Enemy
{
    private const float topSpeed = 7;
    private const float acceleration = 0.3f;
    private const float walkingSpeed = 1.2f;

    private Animator anim;

    private float movementSpeed;
    private bool isChasing;
    private bool isRunning;
    private bool isIdle;
    private bool playerInLoS;
    private bool isPatrolling;
    private bool isPlayingPatrolAction;

    // 0 = idle; 1 = walking right; -1 = walking left
    private int patrolAction;
    
    private void Awake()
    {
        isRunning = false;
        isChasing = false;
        playerInLoS = false;
        isPlayingPatrolAction = false;
        isIdle = true;
        isPatrolling = true;
        patrolAction = 0;
        movementSpeed = 0;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInLoS = true;
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInLoS = false;
            StartCoroutine(CheckIfPlayerIsHiding());
        }
    }

    private void Update()
    {
        PlayVelocityAnimation();
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(movementSpeed));
        anim.SetBool("GroundCheck", IsGrounded());
        anim.SetBool("Running", isRunning);
        anim.SetBool("Idle", isIdle);
        if (isChasing) {
            Flip(GetXVectorByPlayerXPosition(player.transform.position.x));
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayVelocityAnimation()
    {
        if (rb.velocity.x == 0) {
            isRunning = false;
            isPatrolling = true;
        } else if (rb.velocity.x < -walkingSpeed || rb.velocity.x > walkingSpeed) {
            isRunning = true;
            isIdle = false;
            isPatrolling = false;
        }
    }

    private void Move()
    {
        if (isChasing) {
            if (movementSpeed >= topSpeed) {
                movementSpeed = topSpeed - acceleration;
            } else if (movementSpeed <= -topSpeed) {
                movementSpeed = -topSpeed + acceleration;
            } else {
                movementSpeed = movementSpeed + acceleration * GetXVectorByPlayerXPosition(player.transform.position.x);
            }
        } else {
            if (!isPatrolling) {
                if (movementSpeed > -1 && movementSpeed < 1) {
                    movementSpeed = 0;
                    isIdle = true;
                } else if (movementSpeed > 0) {
                    movementSpeed -= acceleration;
                } else if (movementSpeed < 0) {
                    movementSpeed += acceleration;
                }
            } else {
                Patrol();
            }
        }
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void Patrol()
    {
        if (!isPlayingPatrolAction) {
            StartCoroutine(RandomizePatrolAction());
        }
    }

    private void PatrolMove(int action)
    {
        switch (action) {
            default:
                case 0:
                    Flip(-1);
                    movementSpeed = -walkingSpeed;
                    StartCoroutine(WaitBeforeNextMove(Random.Range(1, 4)));
                    break;
                case 1:
                    Flip(1);
                    movementSpeed = walkingSpeed;
                    StartCoroutine(WaitBeforeNextMove(Random.Range(1, 4)));
                    break;
        }
    }

    private IEnumerator WaitBeforeNextMove(int time)
    {
        yield return new WaitForSeconds(time);
        movementSpeed = 0;
        StartCoroutine(EndPatrolAction(Random.Range(1, 3)));
    }

    private IEnumerator EndPatrolAction(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isChasing) {
            isPlayingPatrolAction = false;
        }
    }

    private IEnumerator RandomizePatrolAction()
    {
        isPlayingPatrolAction = true;
        yield return new WaitForSeconds(Random.Range(1, 4));
        patrolAction = Random.Range(0, 2);
        PatrolMove(patrolAction);
    }

    private IEnumerator CheckIfPlayerIsHiding()
    {
        yield return new WaitForSeconds(5);
        if (!playerInLoS) {
            isChasing = false;
            isPatrolling = true;
        }
    }
}
