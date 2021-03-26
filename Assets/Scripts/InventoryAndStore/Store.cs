using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryAndStore
{
    public class Store : MonoBehaviour
    {
        public List<ItemSO> storeBank;

        private void Start()
        {
            foreach (var x in storeBank)
            {
                if (x.tradeState != ItemSO.TradeState.Buyable)
                {
                    storeBank.Remove(x);
                    Debug.LogWarning($"removed {x} from store due to wrong tradestate");
                }
            }
            Inventories.Instance.storeInventory.Add(storeBank);
            Destroy(this);
        }
    }
}
