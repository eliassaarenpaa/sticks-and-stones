using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private Item initialWeapon;
    [SerializeField] private Item initialWeapon2;

    [SerializeField] private Animator weaponAnimator;

    private Item _activeWeapon;

    private void Start()
    {
        inventoryEventChannel.AddItemToBunkerInventory(0, Instantiate(initialWeapon));
        inventoryEventChannel.AddItemToBunkerInventory(2, Instantiate(initialWeapon2));
    }

    private void OnEnable()
    {
        inventoryEventChannel.onAddItemToBunkerInventory.AddListener(OnAddItemToBunkerInv);
    }

    private void OnDisable()
    {
        inventoryEventChannel.onAddItemToBunkerInventory.RemoveListener(OnAddItemToBunkerInv);
    }


    private void OnAddItemToBunkerInv(int index, Item item)
    {
        if(index == 0)
        {
            _activeWeapon = item;
            if (_activeWeapon)
            {
                weaponAnimator.runtimeAnimatorController = _activeWeapon.controller; 
            }
        }   
    }
    public int GetWeaponDamage()
    {
        return _activeWeapon != null ? _activeWeapon.damage : 0;
    }

    public RuntimeAnimatorController GetWeaponAnimator()
    {
        return _activeWeapon.controller;
    }

    public Item GetActiveWeapon()
    {
        return _activeWeapon;
    }

}
