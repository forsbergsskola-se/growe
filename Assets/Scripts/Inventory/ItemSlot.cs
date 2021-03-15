using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemSlot : MonoBehaviour
    {
        public GameObject slotPrefab;
        public List<Item> itemStackable = new List<Item>();
        public List<Item> itemNonStackable = new List<Item>();

        private void Start()
        {
            Inventory.ItemSlot = this;
        }

        public void FindUniqueItems()
        {
            itemStackable.Clear();
            itemNonStackable.Clear();
            
            foreach (var item in Inventory.Items)
            {
                if (item.ItemSo.maxAmount == 1) 
                    itemNonStackable.Add(item);
                
                else if (item.ItemSo.maxAmount > 1)
                {
                    if (!itemStackable.Exists(stackable => stackable.ItemSo == item.ItemSo))
                        itemStackable.Add(item);
                }
            }
        }

        public void CreateItemSlot()
        {
            foreach (Transform child in transform) 
                Destroy(child.gameObject);

            foreach (var nonStackable in itemNonStackable)
            {
                
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.itemSo = nonStackable.ItemSo;
            }
            foreach (var stackable in itemStackable)
            {
                
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.itemSo = stackable.ItemSo;
                itemData.amount = Inventory.CountItem(stackable.ItemSo);
            }
        }

        public void UpdateItemSlots()
        {
            FindUniqueItems();
            CreateItemSlot();
        }
    }
}
