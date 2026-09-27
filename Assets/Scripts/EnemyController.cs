using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float patrolDistance = 3f;

    [Header("Patrol")]
    [SerializeField] private float patrolWaitTime = 1f;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float stopDistance = 0.8f;
    [SerializeField] private Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D enemyCollider;

    private Vector3 startPosition;

    private int moveDirection = -1;

    private float patrolWaitTimer = 0f;
    private bool isWaitingAtPatrolPoint = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();

        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();

            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(enemyCollider, playerCollider);
            }
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        if (player == null)
        {
            Patrol();
            return;
        }

        float distanceToPlayer =
            Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Wait at the patrol point
        if (isWaitingAtPatrolPoint)
        {
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );

            patrolWaitTimer -= Time.fixedDeltaTime;

            if (patrolWaitTimer <= 0f)
            {
                isWaitingAtPatrolPoint = false;

                // Turn around after waiting
                moveDirection *= -1;
            }

            return;
        }

        float distanceFromStart =
            transform.position.x - startPosition.x;

        // Check only the patrol point we are currently moving toward
        bool reachedPatrolPoint =
            (moveDirection == -1 &&
             distanceFromStart <= -patrolDistance)
            ||
            (moveDirection == 1 &&
             distanceFromStart >= patrolDistance);

        if (reachedPatrolPoint)
        {
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );

            isWaitingAtPatrolPoint = true;
            patrolWaitTimer = patrolWaitTime;

            return;
        }

        Move();
    }

    private void ChasePlayer()
    {
        // Stop patrolling when chasing
        isWaitingAtPatrolPoint = false;
        patrolWaitTimer = 0f;

        float horizontalDistance =
            player.position.x - transform.position.x;

        // Stop when close enough to the player
        if (Mathf.Abs(horizontalDistance) <= stopDistance)
        {
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );

            return;
        }

        if (horizontalDistance > 0)
        {
            moveDirection = 1;
        }
        else
        {
            moveDirection = -1;
        }

        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(
            moveDirection * moveSpeed,
            rb.linearVelocity.y
        );

        UpdateFacing();
    }

    private void UpdateFacing()
    {
        // Goblin artwork originally faces left.
        // Flip when moving right.
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = moveDirection == 1;
        }
    }
}