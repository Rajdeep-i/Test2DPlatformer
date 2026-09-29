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

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 20;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D enemyCollider;

    private PlayerHealth playerHealth;

    private Vector3 startPosition;

    private int moveDirection = -1;

    private float patrolWaitTimer = 0f;
    private bool isWaitingAtPatrolPoint = false;

    private float attackTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();

        // Find PlayerHealth from the assigned player
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();

            Collider2D playerCollider = player.GetComponent<Collider2D>();

            if (playerCollider != null && enemyCollider != null)
            {
                Physics2D.IgnoreCollision(
                    enemyCollider,
                    playerCollider
                );
            }
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Reduce attack cooldown
        if (attackTimer > 0f)
        {
            attackTimer -= Time.fixedDeltaTime;
        }

        // Update movement animation
        if (animator != null)
        {
            animator.SetFloat(
                "Speed",
                Mathf.Abs(rb.linearVelocity.x)
            );
        }

        // No player assigned
        if (player == null)
        {
            Patrol();
            return;
        }

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(
            transform.position,
            player.position
        );

        // Player inside detection range
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // =========================
    // PATROL
    // =========================

    private void Patrol()
    {
        // Wait at patrol point
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

                // Turn around
                moveDirection *= -1;
            }

            return;
        }

        // Distance from starting position
        float distanceFromStart =
            transform.position.x - startPosition.x;

        // Check if patrol point is reached
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

    // =========================
    // CHASE PLAYER
    // =========================

    private void ChasePlayer()
    {
        // Stop patrol waiting
        isWaitingAtPatrolPoint = false;
        patrolWaitTimer = 0f;

        // Horizontal distance to player
        float horizontalDistance =
            player.position.x -
            transform.position.x;

        // Player is close enough to attack
        if (Mathf.Abs(horizontalDistance) <= stopDistance)
        {
            // Stop horizontal movement
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );

            // Face the player
            if (horizontalDistance > 0)
            {
                moveDirection = 1;
            }
            else if (horizontalDistance < 0)
            {
                moveDirection = -1;
            }

            UpdateFacing();

            // Attack
            if (attackTimer <= 0f)
            {
                Attack();

                attackTimer = attackCooldown;
            }

            return;
        }

        // Player is to the right
        if (horizontalDistance > 0)
        {
            moveDirection = 1;
        }
        // Player is to the left
        else
        {
            moveDirection = -1;
        }

        Move();
    }

    // =========================
    // ATTACK
    // =========================

    private void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void DealAttackDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    // =========================
    // MOVEMENT
    // =========================

    private void Move()
    {
        rb.linearVelocity = new Vector2(
            moveDirection * moveSpeed,
            rb.linearVelocity.y
        );

        UpdateFacing();
    }

    // =========================
    // FACING
    // =========================

    private void UpdateFacing()
    {
        // Goblin artwork originally faces left.
        // Flip when moving right.

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX =
                moveDirection == 1;
        }
    }
}