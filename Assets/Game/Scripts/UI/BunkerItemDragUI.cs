using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunkerItemDragUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private IngameInventory ingameInventory;
    [SerializeField] private IngameInventory bunkerInventory;
    [SerializeField] private Image selected;

    private Item _selectedItem;
    private Index _originIndex;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public void SelectItem(InventoryType inventoryType , int index)
    {
        _originIndex = index;

        switch (inventoryType)
        {
            case InventoryType.Ingame:
                var slot = ingameInventory.itemSlots[index];

                // Pick up item
                if (_selectedItem == null)
                {
                    // If No Item -> Do nothing
                    if(slot.item == null)
                    {
                        return;
                    }
                    // If item -> Pick up the item
                    else
                    {
                        _selectedItem = slot.item;
                        inventoryEventChannel.RemoveItemFromIngameInventory(index);
                        selected.sprite = _selectedItem.sprite;
                        selected.enabled = true;
                    }

                }
                // If empty -> Drop item
                // If not empty -> Merge item
                else
                {
                    
                }
                

                break;
            case InventoryType.Bunker:
                break;
        }

    }

    private void Update()
    {
        // Get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // Convert the mouse position to canvas overlay space
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePos, canvas.worldCamera, out canvasPos);
        selected.rectTransform.anchoredPosition = canvasPos;
    }

}
