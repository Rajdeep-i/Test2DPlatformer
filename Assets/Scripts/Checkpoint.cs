using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private PlayerDeath playerDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerDeath != null)
            {
                playerDeath.SetRespawnPoint(transform);
            }
        }
    }
}