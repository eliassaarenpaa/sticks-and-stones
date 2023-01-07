using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameInventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private List<SlotUI> slotUIs;

    private void OnEnable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.AddListener(OnAddItemToIngameInventory);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.RemoveListener(OnAddItemToIngameInventory);
    }

    private void OnAddItemToIngameInventory(int index, Item item)
    {
        slotUIs[index]?.UpdateUI(item.sprite);
    }
}
