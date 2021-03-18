using System.Collections.Generic;
using InventoryAndStore;
using UnityEngine;


[System.Serializable]
public class Seedbag
{
    [Header("Seedbag Item Drop Table")]
    public DropTable Items;
    [Space]
    public int price;

    public List<ItemSO> Open(int amount, Inventory inventory)
    {
        List<ItemSO> items = new List<ItemSO>();

        for (int i = 0; i < amount; i++)
        {
            var randomItem = Items.GetRandomItem().Item;
            items.Add(randomItem);
            inventory.Add(randomItem);
        }
        return items;
    }

    public void DroppableItems()
    {
        foreach (var item in Items.ItemDrops())
        {
            Debug.Log(item);
        }
    }
}