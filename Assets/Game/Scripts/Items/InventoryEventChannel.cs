using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InventoryEventChannel : ScriptableObject
{
    public UnityEvent<int, Item> onAddItemToBunkerInventory = new UnityEvent<int, Item>();
    public UnityEvent<int> onRemoveItemFromBunkerInventory = new UnityEvent<int>();

    public UnityEvent<int, Item> onAddItemToIngameInventory = new UnityEvent<int, Item>();
    public UnityEvent<int> onRemoveItemFromIngameInventory = new UnityEvent<int>();
    public Func<InventoryType, int, Item, Item, bool> onMergeItems;

    public Func<Item, Item, bool> onCheckMergeResultIsWeapon;


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

    public bool MergeItems(InventoryType inventoryType, int index, Item item, Item selectedItem)
    {
        return onMergeItems(inventoryType, index, item, selectedItem);
        //return onMergeItems?.Invoke(inventoryType, index, item, selectedItem);
    }

    public bool MergeResultIsWeapon(Item item1, Item item2)
    {
        return onCheckMergeResultIsWeapon(item1, item2);
    }
}
