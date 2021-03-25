using System;
using UnityEngine;

namespace InventoryAndStore
{
    public class Store : MonoBehaviour
    {
        public ItemSO[] storeBank;

        private void Start()
        {
            foreach (var item in storeBank) 
                Inventories.Instance.storeInventory.Add(item);
            Destroy(this);
        }
    }
}
