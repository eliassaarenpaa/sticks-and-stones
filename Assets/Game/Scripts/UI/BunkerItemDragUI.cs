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
    [SerializeField] private BunkerInventory bunkerInventory;
    [SerializeField] private Image selected;

    private Item _selectedItem;
    private Index _originIndex;

    public void OnPressDown(InventoryType inventoryType , int index)
    {
        //_originIndex = index;


        switch (inventoryType)
        {
            case InventoryType.Ingame:
                var ingameSlot = ingameInventory.itemSlots[index];

                // Pick up item
                if (_selectedItem == null)
                {
                    // If No Item -> Do nothing
                    if(ingameSlot.item == null)
                    {
                        return;
                    }
                    // If item -> Pick up the item
                    else
                    {
                        _selectedItem = ingameSlot.item;
                        inventoryEventChannel.RemoveItemFromIngameInventory(index);
                        selected.sprite = _selectedItem.sprite;
                        selected.enabled = true;
                    }

                }
                else
                {
                    // If empty -> Drop item
                    if (ingameSlot.item == null)
                    {
                        inventoryEventChannel.AddItemToIngameInventory(index, _selectedItem);
                        _selectedItem = null;
                        selected.sprite = null;
                        selected.enabled = false;
                    }
                    // If not empty -> Merge item
                    else
                    {
                        var mergeSucceeded = inventoryEventChannel.MergeItems(InventoryType.Ingame, index, ingameSlot.item, _selectedItem);
                        if (mergeSucceeded)
                        {
                            _selectedItem = null;
                            selected.sprite = null;
                            selected.enabled = false;
                        }
                    }
                }


                break;
            case InventoryType.Bunker:

                var bunkerSlot = bunkerInventory.itemSlots[index];

                if(bunkerSlot == null)
                {
                    return;
                }

                // Pick up item
                if (_selectedItem == null)
                {
                    if (index == 0)
                    {
                        // Dont allow to pickup the weapon!
                        return;
                    }

                    // If No Item -> Do nothing
                    if (bunkerSlot.item == null)
                    {
                        return;
                    }
                    // If item -> Pick up the item
                    else
                    {
                        _selectedItem = bunkerSlot.item;
                        inventoryEventChannel.RemoveItemFromBunkerInventory(index);
                        selected.sprite = _selectedItem.sprite;
                        selected.enabled = true;
                    }

                }
                else
                {
                    // If empty -> Drop item
                    if (bunkerSlot.item == null)
                    {
                        inventoryEventChannel.AddItemToBunkerInventory(index, _selectedItem);
                        _selectedItem = null;
                        selected.sprite = null;
                        selected.enabled = false;
                    }
                    // If not empty -> Merge item
                    else
                    {
                        // If trying to merge into weapon slot, but result != weapon -> dont allow merge
                        if(index == 0)
                        {
                            if(!inventoryEventChannel.MergeResultIsWeapon(bunkerSlot.item, _selectedItem))
                            {
                                return;
                            }
                        }

                        var mergeSucceeded = inventoryEventChannel.MergeItems(InventoryType.Bunker, index, bunkerSlot.item, _selectedItem);
                        if (mergeSucceeded)
                        {
                            _selectedItem = null;
                            selected.sprite = null;
                            selected.enabled = false;
                        }
                    }

                }

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
