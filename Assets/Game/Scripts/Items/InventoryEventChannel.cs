using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InventoryEventChannel : ScriptableObject
{
    public UnityEvent<int, Item> onAddItemToBunkerInventory = new UnityEvent<int, Item>();
    public UnityEvent<int> onRemoveItemFromBunkerInventory = new UnityEvent<int>();

    public UnityEvent<int, Item> onAddItemToIngameInventory = new UnityEvent<int, Item>();
    public UnityEvent<int> onRemoveItemFromIngameInventory = new UnityEvent<int>();
    public UnityEvent<InventoryType, int, Item, Item> onMergeItems = new UnityEvent<InventoryType, int, Item, Item>();


    public void AddItemToBunkerInventory(int itemSlotIndex, Item item)
    {
        onAddItemToBunkerInventory?.Invoke(itemSlotIndex, item);
    }
    public void RemoveItemFromBunkerInventory(int itemSlotIndex)
    {
        onRemoveItemFromBunkerInventory?.Invoke(itemSlotIndex);
    }

    public void AddItemToIngameInventory(int itemSlotIndex, Item item)
    {
        onAddItemToIngameInventory?.Invoke(itemSlotIndex, item);
    }

    public void RemoveItemFromIngameInventory(int itemSlotIndex)
    {
        onRemoveItemFromIngameInventory?.Invoke(itemSlotIndex);
    }

    public void MergeItems(InventoryType inventoryType, int index, Item item, Item selectedItem)
    {
        onMergeItems?.Invoke(inventoryType, index, item, selectedItem);
    }
}
