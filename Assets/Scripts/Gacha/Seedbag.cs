using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seedbag
{
    [Header("Seedbag Item Drop Table")]
    public DropTable Items;
    [Space]
    public int price;

    public string Open()
    {
        return Items.GetRandomItem().Item;
    }

    public void DroppableItems()
    {
        foreach (var item in Items.ItemDrops())
        {
            Debug.Log(item);
        }
    }
}