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
        var existingItem = itemSlots[index].item;

        if(existingItem != null)
        {
            item.gameObject.SetActive(false);
            existingItem.transform.position = item.transform.position;
            existingItem.gameObject.SetActive(true);
            itemSlots[index].item = item;
        }
        else
        {
            itemSlots[index].item = item;
            item.gameObject.SetActive(false);
        }
    }

    private void OnRemoveItemFromIngameInventory(int index)
    {
        itemSlots[index] = null;
    }

}
