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
            items.Add(newItemSO);
            itemSlot.UpdateItemSlots();
            
            Debug.Log("Normal Add", this);
        }

        public void Add<T>(IEnumerable<T> list)
        {
            List<ItemSO> convertList = 
                typeof(T) == typeof(ItemSO) ? 
                    new List<ItemSO>(list.Cast<ItemSO>()) : 
                    (from ItemClass itemClass in list select ConvertSO.ClassToSO(itemClass)).ToList();
            
            foreach (ItemSO itemSO in convertList) 
                items.Add(itemSO);
            
            
            Debug.Log("This got called", this);
            
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
    }
}
