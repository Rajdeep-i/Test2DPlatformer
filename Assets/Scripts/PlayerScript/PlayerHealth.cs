using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;

    private int currentHealth;
    private Animator animator;

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        // Setup health ber
        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = 1f;
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    // TAKE DAMAGE

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

        //Update Health bar
        UpdeteHealthBer();

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

    // UPDATE HEALTH BAR
    private void UpdeteHealthBer()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }


    // RESET HEALTH
    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.value = 1f;
        }

        Debug.Log("Health Reset: " + currentHealth);
    }

    // DEATH

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