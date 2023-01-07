using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private IngameInventory ingameInventory;
    [SerializeField] private BunkerInventory bunkerInventory;

    private void OnEnable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.AddListener(OnAddItemToIngameInventory);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onAddItemToIngameInventory.RemoveListener(OnAddItemToIngameInventory);
    }

    private void OnAddItemToIngameInventory(int itemSlotIndex, Item item)
    {

    }
}
