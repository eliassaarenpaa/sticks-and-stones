using Assets.Game.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemMergeSystem : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private List<ItemMergeRecipe> itemMergeRecipes;

    private void OnEnable()
    {
        inventoryEventChannel.onMergeItems += OnMergeItems;
        inventoryEventChannel.onCheckMergeResultIsWeapon += OnCheckMergeResultIsWeapon;
    }

    private void OnDisable()
    {
        inventoryEventChannel.onMergeItems -= OnMergeItems;
        inventoryEventChannel.onCheckMergeResultIsWeapon -= OnCheckMergeResultIsWeapon;
    }
    private bool OnCheckMergeResultIsWeapon(Item item1, Item item2)
    {
        var recipe = FindMergeRecipe(item1, item2);

        if (recipe == default)
        {
            Debug.LogWarning("No Recipe Found!");
            return false;
        }

        return recipe.result.GetComponent<WeaponItem>() != null;
    }

    /// <summary>
    /// Returns true if succeeded
    /// </summary>
    /// <param name="inventoryType"></param>
    /// <param name="index"></param>
    /// <param name="slotItem"></param>
    /// <param name="selectedItem"></param>
    /// <returns></returns>
    private bool OnMergeItems(InventoryType inventoryType, int index, Item slotItem, Item selectedItem)
    {
        var recipe = FindMergeRecipe(slotItem, selectedItem);

        if(recipe == default)
        {
            Debug.LogWarning("No Recipe Found!");
            return false;
        }

        var resultItemPrefab = recipe.result;
        var resultItemInstance = Instantiate(resultItemPrefab);

        switch(inventoryType)
        {
            case InventoryType.Ingame:
                inventoryEventChannel.AddItemToIngameInventory(index, resultItemInstance);
                break;

            case InventoryType.Bunker:
                inventoryEventChannel.AddItemToBunkerInventory(index, resultItemInstance);                
                break;
        }

        return true;
    }

    private ItemMergeRecipe FindMergeRecipe(Item item1, Item item2)
    {
        return itemMergeRecipes.FirstOrDefault(x =>
            x.item1.sprite == item1.sprite && x.item2.sprite == item2.sprite || x.item1.sprite == item2.sprite && x.item2.sprite == item1.sprite
        ) ;
    }

}
