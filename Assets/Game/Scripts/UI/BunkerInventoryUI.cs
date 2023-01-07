using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerInventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private List<SlotUI> slotUIs;

    private void OnEnable()
    {
        inventoryEventChannel.onAddItemToBunkerInventory.AddListener(OnAddItemToBunkerInventory);
        inventoryEventChannel.onRemoveItemFromBunkerInventory.AddListener(OnRemoveItemFromBunkerInventory);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.RemoveListener(OnAddItemToBunkerInventory);
        inventoryEventChannel.onRemoveItemFromBunkerInventory.RemoveListener(OnRemoveItemFromBunkerInventory);
    }

    private void OnAddItemToBunkerInventory(int index, Item item)
    {
        slotUIs[index]?.UpdateUI(item.sprite);
    }

    private void OnRemoveItemFromBunkerInventory(int index)
    {
        slotUIs[index]?.UpdateUI(null);
    }
}
