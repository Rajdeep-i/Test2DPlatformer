using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private bool levelCompleted = false;

    [Header("UI")]
    [SerializeField] private GameObject inGameHUDCanvas;
    [SerializeField] private GameObject levelCompleteCanvas;

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

    // set the Restart Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // this is the part main CompleteLevel
    
    private void CompleteLevel()
    {
        // Stop player movement
        PlayerController playerController = FindFirstObjectByType<PlayerController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Stop player physics
        Rigidbody2D playerRb = playerController.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }

        // Stop movement animation
        Animator animator = playerController.GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isGrounded", false);

            animator.SetFloat("yVelocity", 0);
        }


        // Disable In-Game HUD
        if (inGameHUDCanvas != null)
        {
            inGameHUDCanvas.SetActive(false);
        }

        // Show Level Complete UI
        if (levelCompleteCanvas != null)
        {
            levelCompleteCanvas.SetActive(true);
        }
    }
}