using System;
using UnityEngine;

namespace Inventory_and_Store
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
