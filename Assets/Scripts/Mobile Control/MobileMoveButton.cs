using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private Direction direction;

    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (direction == Direction.Left)
        {
            player.MobileMoveLeft();
        }
        else
        {
            player.MobileMoveRight();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.MobileStop();
    }
}