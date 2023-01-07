using UnityEngine;
using UnityEngine.EventSystems;

public class BunkerSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private InventoryType inventoryType;
    [SerializeField] private BunkerItemDragUI bunkerItemDragUI;
    [SerializeField] private int slotIndex;
    private bool _isHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isHovering)
        {
            bunkerItemDragUI.SelectItem(inventoryType, slotIndex);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

}
