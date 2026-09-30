using UnityEngine;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private PlayerEnergy playerEnergy;
    [SerializeField] private TMP_Text energyText;

    private void Update()
    {
        if (playerEnergy == null || energyText == null)
            return;

        energyText.text = playerEnergy.CurrentEnergy.ToString();
    }
}