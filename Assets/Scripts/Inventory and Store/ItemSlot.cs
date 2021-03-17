using System.Collections.Generic;
using UnityEngine;

namespace Inventory_and_Store
{
    public class ItemSlot : MonoBehaviour
    {
        public GameObject slotPrefab;
        public Inventory inventory;
        public List<Item> uniqueItemStack = new List<Item>();

        private void FindUniqueItems()
        {
            uniqueItemStack.Clear();

            foreach (var item in inventory.Items)
            {
                if (item.ItemSo.maxAmount == 1) 
                    uniqueItemStack.Add(item);
                
                else if (item.ItemSo.maxAmount > 1)
                {
                    if (!uniqueItemStack.Exists(stackable => stackable.ItemSo == item.ItemSo))
                        uniqueItemStack.Add(item);
                }
            }
        }

        private void CreateItemSlot()
        {
            foreach (Transform child in transform) 
                Destroy(child.gameObject);

            foreach (var unique in uniqueItemStack)
            {
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.ItemInfo.ItemSo = unique.ItemSo;
                
                if (unique.ItemSo.maxAmount > 1) 
                    itemData.amount = inventory.CountItem(unique.ItemSo);
            }
        }

        public void UpdateItemSlots()
        {
            FindUniqueItems();
            CreateItemSlot();
        }
    }
}
