using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameInventory : Inventory
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;

    private void OnEnable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.AddListener(OnAddItemToIngameInventory);
        inventoryEventChannel.onRemoveItemFromIngameInventory.AddListener(OnRemoveItemFromIngameInventory);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.RemoveListener(OnAddItemToIngameInventory);
        inventoryEventChannel.onRemoveItemFromIngameInventory.RemoveListener(OnRemoveItemFromIngameInventory);
    }

    private void OnAddItemToIngameInventory(int index, Item item)
    {
        Debug.Log(itemSlots[index]);

        var existingItem = itemSlots[index]?.item;

        if (existingItem != null)
        {
            item.gameObject.SetActive(false);
            existingItem.transform.position = item.transform.position;
            existingItem.gameObject.SetActive(true);

            if (itemSlots[index] != null)
            {
                itemSlots[index].item = item;
            }
        }
        else
        {
            if (itemSlots[index] != null)
            {
                itemSlots[index].item = item;
                item.gameObject.SetActive(false);
            }
        }
    }

    private void OnRemoveItemFromIngameInventory(int index)
    {
        itemSlots[index].item = null;
    }

}
