using System;
using UnityEngine;

namespace InventoryAndStore
{
    public class Store : MonoBehaviour
    {
        public ItemSO[] storeBank;
        public Inventory StoreInventory => GetComponent<Inventory>();

        private void Start()
        {
            foreach (var item in storeBank) 
                StoreInventory.Add(item);
        }
    }
}
