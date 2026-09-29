using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    private Animator animator;
    private Rigidbody2D rb;
    private EnemyController enemyController;

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();
    }

    // Take Damage
    public void TakeDamage(int damage)
    {
        // Don't take damage after death
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Enemy Health: " + currentHealth);

        // Enemy still alive
        if (currentHealth > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
        else
        {
            Die();
        }
    }

    // Death
    private void Die()
    {
        Debug.Log("Enemy died");

        // STOP ENEMY AI
        if (enemyController != null)
        {
            enemyController.enabled = false;
        }

        // STOP PHYSICAL MOVEMENT
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Destroy after 2 seconds
        Destroy(gameObject, 2f);
    }
}