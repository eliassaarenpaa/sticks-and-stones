using System.Collections.Generic;
using UnityEngine;

public class IngameInventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private List<SlotUI> slotUIs;

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
        slotUIs[index]?.UpdateUI(item.sprite);
    }
    private void OnRemoveItemFromIngameInventory(int index)
    {
        slotUIs[index]?.UpdateUI(null);
    }
}
