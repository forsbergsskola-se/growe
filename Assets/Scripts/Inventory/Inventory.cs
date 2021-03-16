using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public static class Inventory
    {
        public static readonly List<Item> Items = new List<Item>();
        public static ItemSlot ItemSlot;

        public static void Add(ItemSO newItemSO)
        {
            var item = new Item();
            if (newItemSO.hasLifeTime)
                item.lifeTime = newItemSO.lifeTimeHoursInInventory;
            item.ItemSo = newItemSO;
            Items.Add(item);
            ItemSlot.UpdateItemSlots();
        }
        public static void Remove(Item removeItem)
        {
            Items.Remove(removeItem);
            ItemSlot.UpdateItemSlots();
        }

        public static int CountItem(ItemSO itemSo)
        {
            return Items.Count
                (item => item.ItemSo.name.StartsWith(itemSo.name));
        }
    }
}
