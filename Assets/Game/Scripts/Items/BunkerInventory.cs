using Assets.Game.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerInventory : Inventory
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private WeaponItem initialWeapon;

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
        var existingItem = itemSlots[index].item;

        if(index == 0)
        {
            SetActiveWeapon(item);
        }

        if (existingItem != null)
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

    private void SetActiveWeapon(Item item)
    {
        Debug.Log("Active weapon = " + item.gameObject.name);
    }

    private void OnRemoveItemFromBunkerInventory(int index)
    {
        itemSlots[index] = null;
    }

    private void Start()
    {

        inventoryEventChannel.AddItemToBunkerInventory(0, Instantiate(initialWeapon));
    }
}
