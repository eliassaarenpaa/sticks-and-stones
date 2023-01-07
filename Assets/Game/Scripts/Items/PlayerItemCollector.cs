using UnityEngine;
using static UnityEditor.Progress;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] InventoryEventChannel inventoryEventChannel;
    [SerializeField] private LayerMask itemLayer;

    private bool _isKey1;
    private bool _isKey2;
    private bool _isKey3;

    private Item _collidingWithItem;

    private void Update()
    {
        _isKey1 = Input.GetKeyDown(KeyCode.Alpha1);
        _isKey2 = Input.GetKeyDown(KeyCode.Alpha2);
        _isKey3 = Input.GetKeyDown(KeyCode.Alpha3);

        if(_collidingWithItem != null)
        {
            if (_isKey1)
            {
                inventoryEventChannel.AddItemToIngameInventory(0, _collidingWithItem);
                if (_collidingWithItem)
                {
                    _collidingWithItem.transform.localScale = Vector3.one;
                    _collidingWithItem = null;
                }
            }
            else if (_isKey2)
            {
                inventoryEventChannel.AddItemToIngameInventory(1, _collidingWithItem);
                if (_collidingWithItem)
                {
                    _collidingWithItem.transform.localScale = Vector3.one;
                    _collidingWithItem = null;
                }
            }
            else if (_isKey3)
            {
                inventoryEventChannel.AddItemToIngameInventory(2, _collidingWithItem);
                if (_collidingWithItem)
                {
                    _collidingWithItem.transform.localScale = Vector3.one;
                    _collidingWithItem = null;
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(1<< collision.gameObject.layer == itemLayer)
        {
            var item = collision.GetComponent<Item>();
            item.transform.localScale = Vector3.one * 1.5f;
            _collidingWithItem = item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == itemLayer)
        {
            collision.transform.localScale = Vector3.one;
            _collidingWithItem = null;
        }
    }

}
