using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class StoreSwitchSellBuy : MonoBehaviour
    {
        private readonly List<ItemSO> _sellableList = new List<ItemSO>();
        private readonly List<ItemSO> _storeItems = new List<ItemSO>();

        private bool _storeSwitch;

        private void FindSellables()
        {
            foreach (ItemSO itemSO in Inventories.Instance.playerInventory.items.Where(itemSO => itemSO.tradeState == ItemSO.TradeState.Sellable))
                _sellableList.Add(itemSO);
        }

        public void OnSwitchButton() {
            _storeSwitch = !_storeSwitch;
            if (_storeSwitch) SwitchToSell();
            else if (!_storeSwitch) SwitchToBuy();
        }

        private void SwitchToSell()
        {
            foreach (ItemSO itemSO in Inventories.Instance.storeInventory.items)
                _storeItems.Add(itemSO);
            
            Inventories.Instance.storeInventory.items.Clear();
            
            FindSellables();
            foreach (ItemSO itemSO in _sellableList)
                Inventories.Instance.storeInventory.Add(itemSO);
            _sellableList.Clear();
        }

        private void SwitchToBuy()
        {
            Inventories.Instance.storeInventory.items.Clear();
            foreach (ItemSO itemSO in _storeItems)
                Inventories.Instance.storeInventory.Add(itemSO);
            
            _storeItems.Clear();
        }
    }
}
