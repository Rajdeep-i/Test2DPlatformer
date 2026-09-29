using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Cooldown")]
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private float nextAttackTime;

    // Called by New Input System
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + attackCooldown;

        // Play attack animation
        animator.SetTrigger("Attack");

        // Deal damage
        Attack();
    }

    private void Attack()
    {
        Debug.Log("Player Attack!");

        if (attackPoint == null)
        {
            Debug.LogWarning("Attack Point is not assigned!");
            return;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
}