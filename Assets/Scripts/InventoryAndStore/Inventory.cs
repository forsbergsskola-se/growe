using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class Inventory : MonoBehaviour
    {
        public List<ItemSO> items = new List<ItemSO>();
        public ItemSlot itemSlot;

        public void Add(ItemSO newItemSO)
        {
            items.Add(newItemSO);
            itemSlot.UpdateItemSlots();
        }
        public void Remove(ItemSO removeItem)
        {
            items.Remove(removeItem);
            itemSlot.UpdateItemSlots();
        }

        public static int CountItem(IEnumerable<ItemSO> list, ItemSO CountSO)
        { 
            return list.Count(item => 
                item.name.StartsWith(CountSO.name) && 
                item.isShiny == CountSO.isShiny);
        }
    }
}
