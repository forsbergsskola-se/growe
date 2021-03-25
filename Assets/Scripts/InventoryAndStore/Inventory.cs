using System.Collections.Generic;
using System.Linq;
using JSON;
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

        public static bool CheckIfIdentical(ItemSO compare1, ItemSO compare2)
        {
            return compare1.name.StartsWith(compare2.name) && 
                   compare1.isShiny == compare2.isShiny;
        }

        public static int CountItem(IEnumerable<ItemSO> list, ItemSO countSO)
        { 
            return list.Count(item => CheckIfIdentical(item, countSO));
        }
    }
}
