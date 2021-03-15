using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 1)]
public class DropTable : ScriptableObject
{
    [SerializeField] private List<ItemDrop> DroppableItems;

    public ItemDrop GetRandomItem()
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
}

[System.Serializable]
public class ItemDrop
{
    public float DropChance;
    public string Item;
}