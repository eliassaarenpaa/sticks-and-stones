using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InventoryEventChannel : ScriptableObject
{
    //public UnityEvent<Inventory, Item> onAddItemToInventory = new UnityEvent<Inventory, Item>();
    //public UnityEvent<Slot, Slot> onCombineItems = new UnityEvent<Slot, Slot>();

    public UnityEvent<int, Item> onAddItemToIngameInventory = new UnityEvent<int, Item>();
    public void AddItemToIngameInventory(int itemSlotIndex, Item item)
    {
        onAddItemToIngameInventory?.Invoke(itemSlotIndex, item);
    }

}
