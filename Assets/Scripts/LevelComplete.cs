using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleted)
            return;

        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            Debug.Log("LEVEL COMPLETE!");

            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        // Stop the player's movement
        PlayerController playerController = FindFirstObjectByType<PlayerController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Stop player physics
        Rigidbody2D playerRb = FindFirstObjectByType<PlayerController>()
            ?.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }
}