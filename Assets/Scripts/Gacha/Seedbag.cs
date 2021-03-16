using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

[System.Serializable]
public class Seedbag
{
    [Header("Seedbag Item Drop Table")]
    public DropTable Items;
    [Space]
    public int price;

    public List<ItemSO> Open(int amount)
    {
        List<ItemSO> items = new List<ItemSO>();

        for (int i = 0; i < amount; i++)
        {
            Item item = new Item();
            items.Add(Items.GetRandomItem().Item);
            return items;
        }

        throw new SystemException();
    }

    public void DroppableItems()
    {
        foreach (var item in Items.ItemDrops())
        {
            Debug.Log(item);
        }
    }
}