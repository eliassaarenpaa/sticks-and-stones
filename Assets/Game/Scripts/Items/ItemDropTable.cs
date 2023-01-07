using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDropTable : ScriptableObject
{
    public List<Item> items;

    public Item GetRandomItem()
    {
        return items[Random.Range(0, items.Count)];
    }
}
