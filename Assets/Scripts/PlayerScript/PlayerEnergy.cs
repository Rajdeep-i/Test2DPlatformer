using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private int maxEnergy = 100;
    [SerializeField] private int currentEnergy = 0;

    public int CurrentEnergy => currentEnergy;
    public int MaxEnergy => maxEnergy;

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;

        currentEnergy = Mathf.Clamp(
            currentEnergy,
            0,
            maxEnergy
        );

        Debug.Log("Energy: " + currentEnergy + " / " + maxEnergy);
    }

    public void RemoveEnergy(int amount)
    {
        currentEnergy -= amount;

        currentEnergy = Mathf.Clamp(
            currentEnergy,
            0,
            maxEnergy
        );

        Debug.Log("Energy: " + currentEnergy + " / " + maxEnergy);
    }

    public void ResetEnergy()
    {
        currentEnergy = 0;

        Debug.Log("Energy Reset");
    }
}