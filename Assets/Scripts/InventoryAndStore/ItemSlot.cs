using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class ItemSlot : MonoBehaviour
    {
        public GameObject slotPrefab;
        public Inventory inventory;
        public List<ItemSO> sortedItems = new List<ItemSO>();

        private void SortItemsByStackable()
        {
            sortedItems.Clear();

            foreach (ItemSO itemSO in inventory.items)
            {
                if (itemSO.maxAmount == 1)
                    sortedItems.Add(itemSO);
                
                else if (itemSO.maxAmount > 1)
                {
                    foreach (var stackable in sortedItems.Where(stackable => !ComparingArbitraryObjects.Compare(stackable, itemSO)))
                        sortedItems.Add(itemSO);

                    //if (!sortedItems.Exists(stackable => stackable.name == itemSO.name))
                      //  sortedItems.Add(itemSO);
                }
            }
        }

        
        
        private void CreateItemSlot()
        {
            foreach (Transform child in transform) 
                Destroy(child.gameObject);

            foreach (ItemSO itemSO in sortedItems)
            {
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.ItemSO = itemSO;
                
                if (itemSO.maxAmount > 1) 
                    itemData.amount = inventory.CountItem(itemSO);
            }
        }

        public void UpdateItemSlots()
        {
            SortItemsByStackable();
            CreateItemSlot();
        }
    }
}
