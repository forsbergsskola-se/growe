using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using InventoryAndStore;

[CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 1)]
public class DropTable : ScriptableObject
{
    [SerializeField] private List<ItemDrop<ItemSO>> DroppableItems;

    public ItemSO GetRandomItem()
    {
        float total = DroppableItems.Sum(drop => drop.DropChance);
        float roll = Random.Range(0, total);

        foreach (var item in DroppableItems)
        {
            float shinyRoll = Random.Range(0, 100);
            if (item.DropChance >= roll)
            {
                if (shinyRoll >= 98)
                {
                    item.Item.isShiny = true;
                    return item.Item;
                }
                else
                {
                    return item.Item;
                }
            }
            roll -= item.DropChance;
        }

        throw new SystemException();
    }
}

[System.Serializable]
public class ItemDrop<TItem>
{
    public TItem Item;
    [Space]
    public float DropChance;
}