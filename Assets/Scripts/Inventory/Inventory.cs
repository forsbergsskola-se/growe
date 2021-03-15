using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public static class Inventory
    {
        public static List<Item> items = new List<Item>();

        public static void Add(ItemSO newItemSO)
        {
            var item = new Item();
            if (newItemSO.hasLifeTime)
                item.lifeTime = newItemSO.lifeTimeHoursInInventory;
            item.ItemSo = newItemSO;
            items.Add(item);
        }
        public static void Remove(Item removeItem)
        {
            items.Remove(removeItem);
        }

        public static int CountItem(ItemSO itemSo)
        {
            return items.Count
                (item => item.ItemSo.itemType != null && item.ItemSo.itemType.StartsWith(itemSo.itemType));
        }
    }
}
