using System;
using System.Collections.Generic;
using System.Linq;
using JSON;
using UnityEngine;

namespace InventoryAndStore
{
    public class Inventory : MonoBehaviour
    {
        private static Currency Currency => FindObjectOfType<Currency>();
        public List<ItemSO> items = new List<ItemSO>();
        public ItemSlot itemSlot;

        public void Add(ItemSO newItemSO)
        {
            items.Add(ChangeName(newItemSO));
            itemSlot.UpdateItemSlots();
        }

        public void Add<T>(IEnumerable<T> list)
        {
            List<ItemSO> convertList = 
                typeof(T) == typeof(ItemSO) ? 
                    new List<ItemSO>(list.Cast<ItemSO>()) : 
                    (from ItemClass itemClass in list select ConvertSO.ClassToSO(itemClass)).ToList();
            
            foreach (ItemSO itemSO in convertList) 
                items.Add(ChangeName(itemSO));
            
            itemSlot.UpdateItemSlots();
        }
        
        public void Remove(ItemSO removeItem)
        {
            items.Remove(removeItem);
            itemSlot.UpdateItemSlots();
        }
        
        public void Remove<T>(IEnumerable<T> list)
        {
            List<ItemSO> convertList = 
                typeof(T) == typeof(ItemSO) ? 
                    new List<ItemSO>(list.Cast<ItemSO>()) : 
                    (from ItemClass itemClass in list select ConvertSO.ClassToSO(itemClass)).ToList();
            
            foreach (ItemSO itemSO in convertList) 
                items.Remove(itemSO);
            
            itemSlot.UpdateItemSlots();
        }

        public void Upload()
        {
            Currency.FireBaseSetUserInventory(this);
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

        public ItemSO ChangeName(ItemSO itemSO)
        {
            if (itemSO == null) return null;
            string input = itemSO.name;
            int index = input.IndexOf("(");
            if (index > 0)
                itemSO.name = input.Substring(0, index);
            return itemSO;
        }
    }
}
