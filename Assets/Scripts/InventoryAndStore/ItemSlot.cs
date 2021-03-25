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
        public List<GameObject> itemSlots = new List<GameObject>();
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
                    if (!sortedItems.Exists(stackable => stackable.name == itemSO.name && stackable.isShiny == itemSO.isShiny))
                        sortedItems.Add(itemSO);
                    
                    if (itemSO.maxAmount < (double)Inventory.CountItem(inventory.items, itemSO)/Inventory.CountItem(sortedItems, itemSO))
                        sortedItems.Add(itemSO);
                }
            }
        }

        
        
        private void CreateItemSlot()
        {
            foreach (GameObject itemSlot in itemSlots) 
                Destroy(itemSlot);
            itemSlots.Clear();
            
            foreach (ItemSO itemSO in sortedItems)
            {
                var newItemSlot = Instantiate(slotPrefab, transform);
                var newItemSlotData = newItemSlot.GetComponent<ItemData>();
                newItemSlotData.ItemSO = itemSO;
                itemSlots.Add(newItemSlot);
                
                if (itemSO.maxAmount > 1 )
                {
                    int count = Inventory.CountItem(inventory.items, itemSO);
                    int alreadyAssigned = 0;
                    
                    if (count > itemSO.maxAmount)
                    {
                        foreach (GameObject go in itemSlots)
                        {
                            ItemData itemData = go.GetComponent<ItemData>();
                            if (itemData.ItemSO.name == itemSO.name && itemData.ItemSO.isShiny == itemSO.isShiny)
                                alreadyAssigned += itemData.amount;
                        }
                        
                        
                    }
                    newItemSlotData.amount = Math.Min(itemSO.maxAmount, count - alreadyAssigned);
                }
            }
        }

        public void UpdateItemSlots()
        {
            SortItemsByStackable();
            CreateItemSlot();
        }
    }
}
