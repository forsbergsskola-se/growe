using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class CreateItemSlot : MonoBehaviour
    {
        public GameObject SlotPrefab;
        private List<Item> foundUniqueItem = new List<Item>();
        
        public void FindUniqueItems()
        {
            foundUniqueItem.Clear();
            foreach (var item in Inventory.items)
            {
                if (item.ItemSo.maxAmount == 1)
                {
                    foundUniqueItem.Add(item);
                }
                else if (item.ItemSo.maxAmount > 1 && !foundUniqueItem.Contains(item))
                {
                    foundUniqueItem.Add(item);
                }
            }
        }

        public void CreateItemSLot()
        {
            foreach (var UniqueItem in foundUniqueItem)
            {
                var newItemSlot = Instantiate(SlotPrefab, transform);
                newItemSlot.GetComponent<ItemData>().ItemSo = UniqueItem.ItemSo;
            }
        }
    }
}
