using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Inventory;

[CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 1)]
public class DropTable : ScriptableObject
{
    [SerializeField] private List<ItemDrop<ItemSO>> DroppableItems;

    public ItemDrop<ItemSO> GetRandomItem()
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

    public List<ItemSO> ItemDrops()
    {
        List<ItemSO> items = new List<ItemSO>();

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

    [Space]
    public float DropChance;
}