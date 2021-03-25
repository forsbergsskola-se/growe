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
        public List<ItemData> itemSlots = new List<ItemData>();
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
                    if (!sortedItems.Exists(stackable => Inventory.CheckIfIdentical(stackable, itemSO))) 
                        sortedItems.Add(itemSO);
                    if (itemSO.maxAmount < (double)Inventory.CountItem(inventory.items, itemSO)/Inventory.CountItem(sortedItems, itemSO)) 
                        sortedItems.Add(itemSO);
                }
            }
        }

        
        
        private void CreateItemSlot()
        {
            foreach (ItemData itemSlot in itemSlots) 
                Destroy(itemSlot.gameObject);
            itemSlots.Clear();
            
            foreach (ItemSO itemSO in sortedItems)
            {
                ItemData newItemSlot = Instantiate(slotPrefab, transform).GetComponent<ItemData>();
                int count = Inventory.CountItem(inventory.items, itemSO);
                int alreadyAssigned = 0;

                newItemSlot.ItemSO = itemSO;
                itemSlots.Add(newItemSlot);

                if (itemSO.maxAmount <= 1) continue;
                if (count > itemSO.maxAmount) 
                    alreadyAssigned += itemSlots
                        .Where(itemData => Inventory.CheckIfIdentical(itemData.ItemSO, itemSO))
                        .Sum(itemData => itemData.amount);
                
                newItemSlot.amount = Math.Min(itemSO.maxAmount, count - alreadyAssigned);
            }
        }

        public void UpdateItemSlots()
        {
            SortItemsByStackable();
            CreateItemSlot();
            
            Inventories.Instance.playerInventory.Upload();
        }
    }
}
