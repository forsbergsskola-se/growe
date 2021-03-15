using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 1)]
public class DropTable : ScriptableObject
{
    [SerializeField] private List<ItemDrop<string>> DroppableItems;

    public ItemDrop<string> GetRandomItem()
    {
        float total = DroppableItems.Sum(drop => drop.DropChance);
        float roll = Random.Range(0, total);

        foreach (var item in DroppableItems)
        {
            if (item.DropChance >= roll)
            {
                return item;
            }

            roll -= item.DropChance;
        }

        throw new SystemException();
    }

    public List<string> ItemDrops()
    {
        List<string> items = new List<string>();

        foreach (var item in DroppableItems)
        {
            items.Add(item.Item);
        }

        return items;
    }
}

[System.Serializable]
public class ItemDrop<TItem>
{
    public TItem Item;
    public float DropChance;
}