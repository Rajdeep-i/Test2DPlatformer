using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    [Header("Death Animation")]
    [SerializeField] private float deathAnimationDuration = 1f;

    private PlayerHealth playerHealth;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isDead = false;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerHealth != null &&
            playerHealth.CurrentHealth <= 0 &&
            !isDead)
        {
            Die();
        }
    }

    // =========================
    // DEATH
    // =========================

    private void Die()
    {
        isDead = true;

        // Disable player collider during death
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }

        // Stop physics movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Wait for death animation
        StartCoroutine(RespawnAfterDeath());
    }

    // =========================
    // RESPAWN
    // =========================

    private IEnumerator RespawnAfterDeath()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("Respawn Point is not assigned!");
            return;
        }

        // Keep collider disabled while moving player
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }

        // Stop any physics movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Move player to respawn point
        transform.position = respawnPoint.position;

        // Reset health
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }

        // Reset death state
        isDead = false;

        // Wait one physics frame before enabling collider
        StartCoroutine(EnableColliderAfterRespawn());

        Debug.Log("Player Respawned");
    }

    // =========================
    // ENABLE COLLIDER
    // =========================

    private IEnumerator EnableColliderAfterRespawn()
    {
        yield return new WaitForFixedUpdate();

        // Reset Animator
        if (animator != null)
        {
            animator.ResetTrigger("Death");

            // Reset movement parameters
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isGrounded", true);
            animator.SetBool("isOnWall", false);
            animator.SetBool("isOnCeiling", false);

            // Reset velocity parameter
            animator.SetFloat("yVelocity", 0f);

            // Reset jump/death triggers
            animator.ResetTrigger("jump");
            animator.ResetTrigger("Death");
        }

        // Enable collider
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = true;
        }

        // Reset physics velocity
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("Player returned to normal state");
    }

    // =========================
    // CHECKPOINT
    // =========================

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        if (newRespawnPoint == null)
        {
            Debug.LogWarning("New Respawn Point is null!");
            return;
        }

        respawnPoint = newRespawnPoint;

        Debug.Log("Respawn Point Updated");
    }
}