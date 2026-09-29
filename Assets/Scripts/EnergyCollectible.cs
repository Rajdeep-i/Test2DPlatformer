using UnityEngine;

public class EnergyCollectible : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int energyAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerEnergy playerEnergy = other.GetComponent<PlayerEnergy>();

        if (playerEnergy != null)
        {
            playerEnergy.AddEnergy(energyAmount);

            Debug.Log("Collected Energy: +" + energyAmount);

            Destroy(gameObject);
        }
    }
}