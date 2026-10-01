using UnityEngine;
using UnityEngine.EventSystems;

public class MobileAttackButton : MonoBehaviour, IPointerDownHandler
{
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = FindFirstObjectByType<PlayerAttack>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerAttack != null)
        {
            playerAttack.MobileAttack();
        }
    }
}