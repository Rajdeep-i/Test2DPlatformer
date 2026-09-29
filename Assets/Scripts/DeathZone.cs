using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        PlayerDeath playerDeath =
            collision.GetComponent<PlayerDeath>();

        if (playerDeath != null)
        {
            Debug.Log("Player entered Death Zone");

            playerDeath.RespawnPlayer();
        }
        else
        {
            Debug.LogWarning(
                "PlayerDeath component not found on Player!"
            );
        }
    }
}