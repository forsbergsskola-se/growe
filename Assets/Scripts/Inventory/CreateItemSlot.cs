using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class CreateItemSlot : MonoBehaviour
    {
        public GameObject slotPrefab;
        public List<Item> itemStackable = new List<Item>();
        public List<Item> itemNonStackable = new List<Item>();

        private void Start()
        {
            Inventory.CreateItemSlot = this;
        }

        public void FindUniqueItems()
        {
            itemStackable.Clear();
            itemNonStackable.Clear();
            
            foreach (var item in Inventory.items)
            {
                if (item.ItemSo.maxAmount == 1)
                {
                    itemNonStackable.Add(item);
                    if (item.ItemSo.itemType == "def")
                    {
                        Debug.Log("added wrong");
                    }
                }
                else if (item.ItemSo.maxAmount > 1)
                {
                    if (itemStackable.Contains(item) == false)
                    {
                        itemStackable.Add(item);
                        if (item.ItemSo.itemType == "def")
                        {
                            Debug.Log("added right");
                        }
                    }
                }
            }
        }

        public void CreateItemSLot()
        {
            foreach (Transform child in transform) 
                Destroy(child.gameObject);

            foreach (var stackable in itemStackable)
            {
                
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.itemSo = stackable.ItemSo;
            }
            foreach (var nonStackable in itemNonStackable)
            {
                
                var newItemSlot = Instantiate(slotPrefab, transform);
                var itemData = newItemSlot.GetComponent<ItemData>();
                itemData.itemSo = nonStackable.ItemSo;
                itemData.amount = Inventory.CountItem(nonStackable.ItemSo);
            }
        }

        public void UpdateItemSlots()
        {
            FindUniqueItems();
            CreateItemSLot();

            foreach (var found in itemStackable)
            {
                Debug.Log(found.ItemSo.itemType);
            }
        }
    }
}
