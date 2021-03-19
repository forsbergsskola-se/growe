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
            ItemSO itemClone = newItemSO;
            items.Add(itemClone);
            itemSlot.UpdateItemSlots();
        }
        public void Remove(ItemSO removeItem)
        {
            items.Remove(removeItem);
            itemSlot.UpdateItemSlots();
        }

        public int CountItem(ItemSO CountSO)
        {
            return items.Count
                (item => item.name.StartsWith(CountSO.name));
        }
    }
}
