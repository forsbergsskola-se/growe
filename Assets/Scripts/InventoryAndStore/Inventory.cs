using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> Items = new List<Item>();
        public ItemSlot itemSlot;

        public void Add(ItemSO newItemSo)
        {
            var item = new Item();
            if (newItemSo.hasLifeTime && newItemSo.tradeState != ItemSO.TradeState.Buyable)
                item.LifeTime = newItemSo.lifeTimeHoursInInventory;
            item.ItemSo = newItemSo;
            Items.Add(item);
            
            itemSlot.UpdateItemSlots();
        }
        public void Remove(Item removeItem)
        {
            Items.Remove(removeItem);
            itemSlot.UpdateItemSlots();
        }

        public int CountItem(ItemSO itemSo)
        {
            return Items.Count
                (item => item.ItemSo.name.StartsWith(itemSo.name));
        }
    }
}
