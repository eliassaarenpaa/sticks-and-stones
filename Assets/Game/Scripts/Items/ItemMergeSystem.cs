using Assets.Game.Scripts.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemMergeSystem : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private List<ItemMergeRecipe> itemMergeRecipes;

    private void OnEnable()
    {
        inventoryEventChannel.onMergeItems.AddListener(OnMergeItems);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onMergeItems.RemoveListener(OnMergeItems);
    }

    private void OnMergeItems(InventoryType inventoryType, int index, Item slotItem, Item selectedItem)
    {
        var recipe = FindMergeRecipe(slotItem, selectedItem);
        var resultItem = recipe.result;
        var resultItemInstance = Instantiate(resultItem);
        resultItemInstance.gameObject.SetActive(false);

        switch(inventoryType)
        {
            case InventoryType.Ingame:

                break;

            case InventoryType.Bunker:
                
                break;
        }
    }

    private ItemMergeRecipe FindMergeRecipe(Item a, Item b)
    {
        return itemMergeRecipes.First(x => x.item1 == a && x.item2 == b || x.item2 == a && x.item1 == b);
    }

}
