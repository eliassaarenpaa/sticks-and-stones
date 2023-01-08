using Assets.Game.Scripts.Items;
using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    [SerializeField] private InventoryEventChannel inventoryEventChannel;
    [SerializeField] private Item initialWeapon;
    [SerializeField] private Animator weaponAnimator;

    private Item _activeWeapon;

    private void Start()
    {
        inventoryEventChannel.AddItemToBunkerInventory(0, Instantiate(initialWeapon));
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
        Debug.Log(index);
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

    public AnimatorController GetWeaponAnimator()
    {
        return _activeWeapon.controller;
    }

    public Item GetActiveWeapon()
    {
        return _activeWeapon;
    }

}
