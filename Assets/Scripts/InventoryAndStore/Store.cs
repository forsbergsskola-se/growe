using System;
using UnityEngine;

namespace InventoryAndStore
{
    public class Store : MonoBehaviour
    {
        public ItemSO[] storeBank;

        private void Start()
        {
            Inventories.Instance.storeInventory.Add(storeBank);
            Destroy(this);
        }
    }
}
