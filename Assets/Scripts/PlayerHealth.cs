using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    private Animator animator;

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    // =========================
    // TAKE DAMAGE
    // =========================

    public void TakeDamage(int damage)
    {
        // Don't take damage if already dead
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Player Health: " + currentHealth);

        // Player still alive
        if (currentHealth > 0)
        {
            // Play Hit animation
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
        else
        {
            // Player died
            Die();
        }
    }

    // =========================
    // RESET HEALTH
    // =========================

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        Debug.Log("Health Reset: " + currentHealth);
    }

    // =========================
    // DEATH
    // =========================

    private void Die()
    {
        Debug.Log("Player Died");

        // Play Death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
    }
}